// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapUriActionTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var action = new ImagemapUriAction()
                {
                    Url = new Uri("https://foo.bar"),
                    Area = new ImagemapArea(0, 10, 20, 30)
                };

                var serialized = JsonSerializer.SerializeObject(action);
                Assert.AreEqual(@"{""type"":""uri"",""linkUri"":""https://foo.bar"",""area"":{""x"":0,""y"":10,""width"":20,""height"":30}}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var action = new ImagemapUriAction(new Uri("https://foo.bar"), new ImagemapArea(1, 2, 3, 4));

                Assert.IsNotNull(action.Area);
                Assert.AreEqual("https://foo.bar/", action.Url.ToString());
                Assert.AreEqual(1, action.Area.X);
                Assert.AreEqual(2, action.Area.Y);
                Assert.AreEqual(3, action.Area.Width);
                Assert.AreEqual(4, action.Area.Height);
            }

            [TestMethod]
            public void ShouldSetTheArea()
            {
                var action = new ImagemapUriAction(new Uri("https://foo.bar"), 1, 2, 3, 4);

                Assert.IsNotNull(action.Area);
                Assert.AreEqual("https://foo.bar/", action.Url.ToString());
                Assert.AreEqual(1, action.Area.X);
                Assert.AreEqual(2, action.Area.Y);
                Assert.AreEqual(3, action.Area.Width);
                Assert.AreEqual(4, action.Area.Height);
            }

            [TestMethod]
            public void ShouldSetTheUrl()
            {
                var action = new ImagemapUriAction("https://foo.bar", new ImagemapArea(1, 2, 3, 4));

                Assert.IsNotNull(action.Area);
                Assert.AreEqual("https://foo.bar/", action.Url.ToString());
                Assert.AreEqual(1, action.Area.X);
                Assert.AreEqual(2, action.Area.Y);
                Assert.AreEqual(3, action.Area.Width);
                Assert.AreEqual(4, action.Area.Height);
            }

            [TestMethod]
            public void ShouldSetTheUrlAndArea()
            {
                var action = new ImagemapUriAction("https://foo.bar", 1, 2, 3, 4);

                Assert.IsNotNull(action.Area);
                Assert.AreEqual("https://foo.bar/", action.Url.ToString());
                Assert.AreEqual(1, action.Area.X);
                Assert.AreEqual(2, action.Area.Y);
                Assert.AreEqual(3, action.Area.Width);
                Assert.AreEqual(4, action.Area.Height);
            }
        }
    }
}