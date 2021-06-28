// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class AudioMessageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var message = new AudioMessage()
                {
                    Url = new Uri("https://foo.url"),
                    Duration = 10000
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.AreEqual(@"{""type"":""audio"",""originalContentUrl"":""https://foo.url"",""duration"":10000}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var message = new AudioMessage(new Uri("https://foo.url"), 1000);

                Assert.AreEqual("https://foo.url/", message.Url.ToString());
                Assert.AreEqual(1000, message.Duration);
            }

            [TestMethod]
            public void ShouldConvertStringUrl()
            {
                var message = new AudioMessage("https://foo.url", 1000);

                Assert.AreEqual("https://foo.url/", message.Url.ToString());
                Assert.AreEqual(1000, message.Duration);
            }
        }
    }
}