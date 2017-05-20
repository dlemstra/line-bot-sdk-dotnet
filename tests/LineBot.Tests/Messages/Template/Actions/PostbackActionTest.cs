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
    public class PostbackActionTest
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            PostbackAction action = new PostbackAction
            {
                Label = "Foo",
                Data = "Bar",
                Text = "Test"
            };

            string serialized = JsonConvert.SerializeObject(action);
            Assert.AreEqual(@"{""type"":""postback"",""label"":""Foo"",""data"":""Bar"",""text"":""Test""}", serialized);
        }

        [TestMethod]
        public void Label_Null_ThrowsException()
        {
            PostbackAction action = new PostbackAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
            {
                action.Label = null;
            });
        }

        [TestMethod]
        public void Label_Empty_ThrowsException()
        {
            PostbackAction action = new PostbackAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
            {
                action.Label = string.Empty;
            });
        }

        [TestMethod]
        public void Label_MoreThan20Chars_ThrowsException()
        {
            PostbackAction action = new PostbackAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be longer than 20 characters.", () =>
            {
                action.Label = new string('x', 21);
            });
        }

        [TestMethod]
        public void Label_20Chars_ThrowsNoException()
        {
            string value = new string('x', 20);

            PostbackAction action = new PostbackAction()
            {
                Label = value
            };

            Assert.AreEqual(value, action.Label);
        }

        [TestMethod]
        public void Data_Null_ThrowsException()
        {
            PostbackAction action = new PostbackAction();

            ExceptionAssert.Throws<InvalidOperationException>("The data cannot be null or whitespace.", () =>
            {
                action.Data = null;
            });
        }

        [TestMethod]
        public void Data_Empty_ThrowsException()
        {
            PostbackAction action = new PostbackAction();

            ExceptionAssert.Throws<InvalidOperationException>("The data cannot be null or whitespace.", () =>
            {
                action.Data = string.Empty;
            });
        }

        [TestMethod]
        public void Data_MoreThan300Chars_ThrowsException()
        {
            PostbackAction action = new PostbackAction();

            ExceptionAssert.Throws<InvalidOperationException>("The data cannot be longer than 300 characters.", () =>
            {
                action.Data = new string('x', 301);
            });
        }

        [TestMethod]
        public void Data_300Chars_ThrowsNoException()
        {
            string value = new string('x', 300);

            PostbackAction action = new PostbackAction()
            {
                Data = value
            };

            Assert.AreEqual(value, action.Data);
        }

        [TestMethod]
        public void Text_Null_ThrowsNoException()
        {
            PostbackAction action = new PostbackAction()
            {
                Text = null
            };
        }

        [TestMethod]
        public void Text_MoreThan300Chars_ThrowsException()
        {
            PostbackAction action = new PostbackAction();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 300 characters.", () =>
            {
                action.Text = new string('x', 301);
            });
        }

        [TestMethod]
        public void Text_300Chars_ThrowsNoException()
        {
            string value = new string('x', 300);

            PostbackAction action = new PostbackAction()
            {
                Text = value
            };

            Assert.AreEqual(value, action.Text);
        }
    }
}
