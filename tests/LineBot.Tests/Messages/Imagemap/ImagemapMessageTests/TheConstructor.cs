// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapMessageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative",
                    Actions = new ImagemapAction[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                        new ImagemapUriAction("https://bar.foo", 5, 6, 7, 8),
                        new ImagemapUriAction("https://bar.foo", new ImagemapArea(9, 10, 11, 12))
                    }
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.AreEqual(@"{""type"":""imagemap"",""baseUrl"":""https://foo.bar"",""altText"":""Alternative"",""baseSize"":{""width"":1040,""height"":1040},""actions"":[{""type"":""message"",""text"":""Text"",""area"":{""x"":1,""y"":2,""width"":3,""height"":4}},{""type"":""uri"",""linkUri"":""https://bar.foo"",""area"":{""x"":5,""y"":6,""width"":7,""height"":8}},{""type"":""uri"",""linkUri"":""https://bar.foo"",""area"":{""x"":9,""y"":10,""width"":11,""height"":12}}]}", serialized);
            }
        }
    }
}