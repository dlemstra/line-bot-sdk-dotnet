// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageMessageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var message = new ImageMessage()
                {
                    Url = new Uri("https://foo.url"),
                    PreviewUrl = new Uri("https://foo.previewUrl"),
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.AreEqual(@"{""type"":""image"",""originalContentUrl"":""https://foo.url"",""previewImageUrl"":""https://foo.previewUrl""}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var message = new ImageMessage(new Uri("https://foo.url"), new Uri("https://foo.previewUrl"));

                Assert.AreEqual("https://foo.url/", message.Url.ToString());
                Assert.AreEqual("https://foo.previewurl/", message.PreviewUrl.ToString());
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