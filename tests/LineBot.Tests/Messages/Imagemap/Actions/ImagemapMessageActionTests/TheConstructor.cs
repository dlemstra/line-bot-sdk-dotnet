// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class ImagemapMessageActionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var action = new ImagemapMessageAction()
                {
                    Text = "Correct",
                    Area = new ImagemapArea(0, 10, 20, 30)
                };

                var serialized = JsonSerializer.SerializeObject(action);
                Assert.Equal(@"{""type"":""message"",""text"":""Correct"",""area"":{""x"":0,""y"":10,""width"":20,""height"":30}}", serialized);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var action = new ImagemapMessageAction("test", new ImagemapArea(1, 2, 3, 4));

                Assert.NotNull(action.Area);
                Assert.Equal("test", action.Text);
                Assert.Equal(1, action.Area.X);
                Assert.Equal(2, action.Area.Y);
                Assert.Equal(3, action.Area.Width);
                Assert.Equal(4, action.Area.Height);
            }

            [Fact]
            public void ShouldSetTheArea()
            {
                var action = new ImagemapMessageAction("test", 1, 2, 3, 4);

                Assert.NotNull(action.Area);
                Assert.Equal("test", action.Text);
                Assert.Equal(1, action.Area.X);
                Assert.Equal(2, action.Area.Y);
                Assert.Equal(3, action.Area.Width);
                Assert.Equal(4, action.Area.Height);
            }

            [Fact]
            public void ShouldConvertStringUrl()
            {
                var message = new ImageMessage("https://foo.url", "https://foo.previewUrl");

                Assert.Equal("https://foo.url/", message.Url.ToString());
                Assert.Equal("https://foo.previewurl/", message.PreviewUrl.ToString());
            }
        }
    }
}
