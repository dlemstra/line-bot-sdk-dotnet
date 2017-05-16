// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    [TestClass]
    public class ImagemapMessageTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            ImagemapMessage message = new ImagemapMessage()
            {
                BaseUrl = new Uri("https://foo.bar"),
                BaseSize = new ImagemapSize(1040, 1040),
                AlternativeText = "Alternative",
                Actions = new ImagemapAction[]
                {
                    new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    new ImagemapUriAction("https://bar.foo", 5, 6, 7, 8),
                    new ImagemapUriAction("https://bar.foo")
                    {
                        Area = new ImagemapArea(9, 10, 11, 12)
                    }
                }
            };

            string serialized = JsonConvert.SerializeObject(message);
            Assert.AreEqual(@"{""type"":""imagemap"",""baseUrl"":""https://foo.bar"",""altText"":""Alternative"",""baseSize"":{""width"":1040,""height"":1040},""actions"":[{""type"":""message"",""text"":""Text"",""area"":{""x"":1,""y"":2,""width"":3,""height"":4}},{""type"":""uri"",""linkUri"":""https://bar.foo"",""area"":{""x"":5,""y"":6,""width"":7,""height"":8}},{""type"":""uri"",""linkUri"":""https://bar.foo"",""area"":{""x"":9,""y"":10,""width"":11,""height"":12}}]}", serialized);
        }

        [TestMethod]
        public void BaseUrl_Null_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be null.", () =>
            {
                message.BaseUrl = null;
            });
        }

        [TestMethod]
        public void BaseUrl_NotHttps_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The base url should use the https scheme.", () =>
            {
                message.BaseUrl = new Uri("http://foo.bar");
            });
        }

        [TestMethod]
        public void BaseUrl_MoreThan1000Chars_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be longer than 1000 characters.", () =>
            {
                message.BaseUrl = new Uri("https://foo.bar/" + new string('x', 985));
            });
        }

        [TestMethod]
        public void BaseUrl_1000Chars_ThrowsNoException()
        {
            Uri value = new Uri("https://foo.bar/" + new string('x', 984));

            ImagemapMessage message = new ImagemapMessage()
            {
                BaseUrl = value
            };

            Assert.AreEqual(value, message.BaseUrl);
        }

        [TestMethod]
        public void BaseSize_Null_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The base size cannot be null.", () =>
            {
                message.BaseSize = null;
            });
        }

        [TestMethod]
        public void AlternativeText_Null_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
            {
                message.AlternativeText = null;
            });
        }

        [TestMethod]
        public void AlternativeText_Empty_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
            {
                message.AlternativeText = string.Empty;
            });
        }

        [TestMethod]
        public void AlternativeText_MoreThan400Chars_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be longer than 400 characters.", () =>
            {
                message.AlternativeText = new string('x', 401);
            });
        }

        [TestMethod]
        public void AlternativeText_400Chars_ThrowsNoException()
        {
            string value = new string('x', 400);

            ImagemapMessage message = new ImagemapMessage()
            {
                AlternativeText = value
            };

            Assert.AreEqual(value, message.AlternativeText);
        }

        [TestMethod]
        public void Actions_Null_ThrowsException()
        {
            ImagemapMessage message = new ImagemapMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
            {
                message.Actions = null;
            });
        }
    }
}
