// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapUriActionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var action = new ImagemapUriAction()
                {
                    Url = new Uri("https://foo.bar"),
                    Area = new ImagemapArea(0, 10, 20, 30)
                };

                var serialized = JsonSerializer.SerializeObject(action);
                Assert.Equal(@"{""type"":""uri"",""linkUri"":""https://foo.bar"",""area"":{""x"":0,""y"":10,""width"":20,""height"":30}}", serialized);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var action = new ImagemapUriAction(new Uri("https://foo.bar"), new ImagemapArea(1, 2, 3, 4));

                Assert.NotNull(action.Area);
                Assert.Equal("https://foo.bar/", action.Url.ToString());
                Assert.Equal(1, action.Area.X);
                Assert.Equal(2, action.Area.Y);
                Assert.Equal(3, action.Area.Width);
                Assert.Equal(4, action.Area.Height);
            }

            [Fact]
            public void ShouldSetTheArea()
            {
                var action = new ImagemapUriAction(new Uri("https://foo.bar"), 1, 2, 3, 4);

                Assert.NotNull(action.Area);
                Assert.Equal("https://foo.bar/", action.Url.ToString());
                Assert.Equal(1, action.Area.X);
                Assert.Equal(2, action.Area.Y);
                Assert.Equal(3, action.Area.Width);
                Assert.Equal(4, action.Area.Height);
            }

            [Fact]
            public void ShouldSetTheUrl()
            {
                var action = new ImagemapUriAction("https://foo.bar", new ImagemapArea(1, 2, 3, 4));

                Assert.NotNull(action.Area);
                Assert.Equal("https://foo.bar/", action.Url.ToString());
                Assert.Equal(1, action.Area.X);
                Assert.Equal(2, action.Area.Y);
                Assert.Equal(3, action.Area.Width);
                Assert.Equal(4, action.Area.Height);
            }

            [Fact]
            public void ShouldSetTheUrlAndArea()
            {
                var action = new ImagemapUriAction("https://foo.bar", 1, 2, 3, 4);

                Assert.NotNull(action.Area);
                Assert.Equal("https://foo.bar/", action.Url.ToString());
                Assert.Equal(1, action.Area.X);
                Assert.Equal(2, action.Area.Y);
                Assert.Equal(3, action.Area.Width);
                Assert.Equal(4, action.Area.Height);
            }
        }
    }
}
