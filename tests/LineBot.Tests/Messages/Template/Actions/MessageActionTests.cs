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

namespace Line.Tests.Messages.Template.Actions
{
    [TestClass]
    public class MessageActionTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            MessageAction action = new MessageAction
            {
                Label = "Foo",
                Text = "Test"
            };

            string serialized = JsonConvert.SerializeObject(action);
            Assert.AreEqual(@"{""type"":""message"",""label"":""Foo"",""text"":""Test""}", serialized);
        }

        [TestMethod]
        public void Label_Null_ThrowsException()
        {
            MessageAction action = new MessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
            {
                action.Label = null;
            });
        }

        [TestMethod]
        public void Label_Empty_ThrowsException()
        {
            MessageAction action = new MessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
            {
                action.Label = string.Empty;
            });
        }

        [TestMethod]
        public void Label_MoreThan20Chars_ThrowsException()
        {
            MessageAction action = new MessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be longer than 20 characters.", () =>
            {
                action.Label = new string('x', 21);
            });
        }

        [TestMethod]
        public void Label_20Chars_ThrowsNoException()
        {
            string value = new string('x', 20);

            MessageAction action = new MessageAction()
            {
                Label = value
            };

            Assert.AreEqual(value, action.Label);
        }

        [TestMethod]
        public void Text_Null_ThrowsException()
        {
            MessageAction action = new MessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                action.Text = null;
            });
        }

        [TestMethod]
        public void Text_Empty_ThrowsException()
        {
            MessageAction action = new MessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                action.Text = string.Empty;
            });
        }

        [TestMethod]
        public void Text_MoreThan300Chars_ThrowsException()
        {
            MessageAction action = new MessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 300 characters.", () =>
            {
                action.Text = new string('x', 301);
            });
        }

        [TestMethod]
        public void Text_300Chars_ThrowsNoException()
        {
            string value = new string('x', 300);

            MessageAction action = new MessageAction()
            {
                Text = value
            };

            Assert.AreEqual(value, action.Text);
        }
    }
}
