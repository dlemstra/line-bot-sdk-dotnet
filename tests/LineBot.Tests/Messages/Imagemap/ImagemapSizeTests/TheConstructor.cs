// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapSizeTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var size = new ImagemapSize()
                {
                    Width = 10,
                    Height = 20
                };

                var serialized = JsonSerializer.SerializeObject(size);
                Assert.AreEqual(@"{""width"":10,""height"":20}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var message = new ImagemapSize(100, 200);

                Assert.AreEqual(100, message.Width);
                Assert.AreEqual(200, message.Height);
            }
        }
    }
}
