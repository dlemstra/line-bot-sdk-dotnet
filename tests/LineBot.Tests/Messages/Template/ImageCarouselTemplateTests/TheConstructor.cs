// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageCarouselTemplateTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var template = new ImageCarouselTemplate();

                string serialized = JsonSerializer.SerializeObject(template);
                Assert.AreEqual(@"{""type"":""image_carousel""}", serialized);
            }
        }
    }
}
