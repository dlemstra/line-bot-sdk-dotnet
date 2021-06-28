// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapMessageActionTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var action = new ImagemapMessageAction()
                {
                    Text = "Correct",
                    Area = new ImagemapArea(0, 10, 20, 30)
                };

                var serialized = JsonSerializer.SerializeObject(action);
                Assert.AreEqual(@"{""type"":""message"",""text"":""Correct"",""area"":{""x"":0,""y"":10,""width"":20,""height"":30}}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var action = new ImagemapMessageAction("test", new ImagemapArea(1, 2, 3, 4));

                Assert.IsNotNull(action.Area);
                Assert.AreEqual("test", action.Text);
                Assert.AreEqual(1, action.Area.X);
                Assert.AreEqual(2, action.Area.Y);
                Assert.AreEqual(3, action.Area.Width);
                Assert.AreEqual(4, action.Area.Height);
            }

            [TestMethod]
            public void ShouldSetTheArea()
            {
                var action = new ImagemapMessageAction("test", 1, 2, 3, 4);

                Assert.IsNotNull(action.Area);
                Assert.AreEqual("test", action.Text);
                Assert.AreEqual(1, action.Area.X);
                Assert.AreEqual(2, action.Area.Y);
                Assert.AreEqual(3, action.Area.Width);
                Assert.AreEqual(4, action.Area.Height);
            }

            [TestMethod]
            public void ShouldConvertStringUrl()
            {
                var message = new ImageMessage("https://foo.url", "https://foo.previewUrl");

                Assert.AreEqual("https://foo.url/", message.Url.ToString());
                Assert.AreEqual("https://foo.previewurl/", message.PreviewUrl.ToString());
            }
        }
    }
}