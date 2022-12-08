// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class ImagemapSizeTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var size = new ImagemapSize()
                {
                    Width = 10,
                    Height = 20
                };

                var serialized = JsonSerializer.SerializeObject(size);
                Assert.Equal(@"{""width"":10,""height"":20}", serialized);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var message = new ImagemapSize(100, 200);

                Assert.Equal(100, message.Width);
                Assert.Equal(200, message.Height);
            }
        }
    }
}
