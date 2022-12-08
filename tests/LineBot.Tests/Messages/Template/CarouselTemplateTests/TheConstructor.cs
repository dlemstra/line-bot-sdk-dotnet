// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class CarouselTemplateTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var template = new CarouselTemplate();

                var serialized = JsonSerializer.SerializeObject(template);
                Assert.Equal(@"{""type"":""carousel"",""imageAspectRatio"":""rectangle"",""imageSize"":""cover""}", serialized);
            }
        }
    }
}
