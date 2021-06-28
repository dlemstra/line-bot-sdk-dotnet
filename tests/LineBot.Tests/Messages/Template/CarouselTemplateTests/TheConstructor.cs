// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class CarouselTemplateTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var template = new CarouselTemplate();

                string serialized = JsonSerializer.SerializeObject(template);
                Assert.AreEqual(@"{""type"":""carousel"",""imageAspectRatio"":""rectangle"",""imageSize"":""cover""}", serialized);
            }
        }
    }
}
