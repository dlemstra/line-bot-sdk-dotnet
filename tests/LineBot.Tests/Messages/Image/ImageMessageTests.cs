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
    public class ImageMessageTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            ImageMessage message = new ImageMessage("https://foo.url", "https://foo.previewUrl");

            string serialized = JsonConvert.SerializeObject(message);
            Assert.AreEqual(@"{""type"":""image"",""originalContentUrl"":""https://foo.url"",""previewImageUrl"":""https://foo.previewUrl""}", serialized);
        }

        [TestMethod]
        public void Url_Null_ThrowsException()
        {
            ImageMessage message = new ImageMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                message.Url = null;
            });
        }

        [TestMethod]
        public void Url_NotHttps_ThrowsException()
        {
            ImageMessage message = new ImageMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url should use the https scheme.", () =>
            {
                message.Url = new Uri("http://foo.bar");
            });
        }

        [TestMethod]
        public void Url_MoreThan2000Chars_ThrowsException()
        {
            ImageMessage message = new ImageMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 2000 characters.", () =>
            {
                message.Url = new Uri("https://foo.bar/" + new string('x', 1985));
            });
        }

        [TestMethod]
        public void Url_2000Chars_ThrowsNoException()
        {
            Uri value = new Uri("https://foo.bar/" + new string('x', 1984));

            ImageMessage message = new ImageMessage()
            {
                Url = value
            };

            Assert.AreEqual(value, message.Url);
        }

        [TestMethod]
        public void PreviewUrl_Null_ThrowsException()
        {
            ImageMessage message = new ImageMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                message.PreviewUrl = null;
            });
        }

        [TestMethod]
        public void PreviewUrl_NotHttps_ThrowsException()
        {
            ImageMessage message = new ImageMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url should use the https scheme.", () =>
            {
                message.PreviewUrl = new Uri("http://foo.bar");
            });
        }

        [TestMethod]
        public void PreviewUrl_MoreThan2000Chars_ThrowsException()
        {
            ImageMessage message = new ImageMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 2000 characters.", () =>
            {
                message.PreviewUrl = new Uri("https://foo.bar/" + new string('x', 1985));
            });
        }

        [TestMethod]
        public void PreviewUrl_2000Chars_ThrowsNoException()
        {
            Uri value = new Uri("https://foo.bar/" + new string('x', 1984));

            ImageMessage message = new ImageMessage()
            {
                PreviewUrl = value
            };

            Assert.AreEqual(value, message.PreviewUrl);
        }
    }
}
