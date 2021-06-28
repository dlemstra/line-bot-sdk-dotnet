// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapAreaTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
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
                Assert.AreEqual(@"{""x"":10,""y"":20,""width"":30,""height"":40}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var action = new ImagemapArea(1, 2, 3, 4);

                Assert.AreEqual(1, action.X);
                Assert.AreEqual(2, action.Y);
                Assert.AreEqual(3, action.Width);
                Assert.AreEqual(4, action.Height);
            }
        }
    }
}
