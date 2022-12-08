// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class ImagemapAreaTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var area = new ImagemapArea()
                {
                    X = 10,
                    Y = 20,
                    Width = 30,
                    Height = 40
                };

                var serialized = JsonSerializer.SerializeObject(area);
                Assert.Equal(@"{""x"":10,""y"":20,""width"":30,""height"":40}", serialized);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var action = new ImagemapArea(1, 2, 3, 4);

                Assert.Equal(1, action.X);
                Assert.Equal(2, action.Y);
                Assert.Equal(3, action.Width);
                Assert.Equal(4, action.Height);
            }
        }
    }
}
