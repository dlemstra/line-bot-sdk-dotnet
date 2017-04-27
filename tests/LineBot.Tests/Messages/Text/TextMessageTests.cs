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
    public class TextMessageTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            TextMessage textMessage = new TextMessage()
            {
                Text = "Correct"
            };

            string serialized = JsonConvert.SerializeObject(textMessage);
            Assert.AreEqual(@"{""type"":""text"",""text"":""Correct""}", serialized);
        }

        [TestMethod]
        public void Text_Null_ThrowsException()
        {
            TextMessage textMessage = new TextMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or empty.", () =>
            {
                textMessage.Text = null;
            });
        }

        [TestMethod]
        public void Text_Empty_ThrowsException()
        {
            TextMessage textMessage = new TextMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or empty.", () =>
            {
                textMessage.Text = string.Empty;
            });
        }

        [TestMethod]
        public void Text_MoreThan2000Chars_ThrowsException()
        {
            TextMessage textMessage = new TextMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 2000 characters.", () =>
            {
                textMessage.Text = new string('x', 2001);
            });
        }

        [TestMethod]
        public void Text_2000Chars_ThrowsNoException()
        {
            string value = new string('x', 2000);

            TextMessage textMessage = new TextMessage()
            {
                Text = value
            };

            Assert.AreEqual(value, textMessage.Text);
        }
    }
}
