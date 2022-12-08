// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class ImageCarouselTemplateTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var template = new ImageCarouselTemplate();

                var serialized = JsonSerializer.SerializeObject(template);
                Assert.Equal(@"{""type"":""image_carousel""}", serialized);
            }
        }
    }
}
