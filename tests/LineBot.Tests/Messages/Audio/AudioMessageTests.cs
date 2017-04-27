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
    public class AudioMessageTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            AudioMessage message = new AudioMessage("https://foo.url", 10000);

            string serialized = JsonConvert.SerializeObject(message);
            Assert.AreEqual(@"{""type"":""audio"",""originalContentUrl"":""https://foo.url"",""duration"":10000}", serialized);
        }

        [TestMethod]
        public void Url_Null_ThrowsException()
        {
            AudioMessage message = new AudioMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                message.Url = null;
            });
        }

        [TestMethod]
        public void Url_NotHttps_ThrowsException()
        {
            AudioMessage message = new AudioMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url should use the https scheme.", () =>
            {
                message.Url = new Uri("http://foo.bar");
            });
        }

        [TestMethod]
        public void Url_MoreThan1000Chars_ThrowsException()
        {
            AudioMessage message = new AudioMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 1000 characters.", () =>
            {
                message.Url = new Uri("https://foo.bar/" + new string('x', 985));
            });
        }

        [TestMethod]
        public void Url_1000Chars_ThrowsNoException()
        {
            Uri value = new Uri("https://foo.bar/" + new string('x', 984));

            AudioMessage message = new AudioMessage()
            {
                Url = value
            };

            Assert.AreEqual(value, message.Url);
        }

        [TestMethod]
        public void Duration_Zero_ThrowsException()
        {
            AudioMessage message = new AudioMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
            {
                message.Duration = 0;
            });
        }

        [TestMethod]
        public void Duration_MinusOne_ThrowsException()
        {
            AudioMessage message = new AudioMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
            {
                message.Duration = -1;
            });
        }

        [TestMethod]
        public void Duration_MoreThan59999Milliseconds_ThrowsException()
        {
            AudioMessage message = new AudioMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The duration cannot be longer than 1 minute.", () =>
            {
                message.Duration = 60000;
            });
        }

        [TestMethod]
        public void Duration_59999Milliseconds_ThrowsException()
        {
            int value = 59999;

            AudioMessage message = new AudioMessage()
            {
                Duration = value
            };

            Assert.AreEqual(value, message.Duration);
        }
    }
}
