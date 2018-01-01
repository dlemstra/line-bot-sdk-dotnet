// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
    public class ImagemapMessageActionTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            ImagemapMessageAction action = new ImagemapMessageAction()
            {
                Text = "Correct",
                Area = new ImagemapArea(0, 10, 20, 30)
            };

            string serialized = JsonConvert.SerializeObject(action);
            Assert.AreEqual(@"{""type"":""message"",""text"":""Correct"",""area"":{""x"":0,""y"":10,""width"":20,""height"":30}}", serialized);
        }

        [TestMethod]
        public void Text_Null_ThrowsException()
        {
            ImagemapMessageAction action = new ImagemapMessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                action.Text = null;
            });
        }

        [TestMethod]
        public void Text_Empty_ThrowsException()
        {
            ImagemapMessageAction action = new ImagemapMessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                action.Text = string.Empty;
            });
        }

        [TestMethod]
        public void Text_MoreThan400Chars_ThrowsException()
        {
            ImagemapMessageAction action = new ImagemapMessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 400 characters.", () =>
            {
                action.Text = new string('x', 401);
            });
        }

        [TestMethod]
        public void Text_400Chars_ThrowsNoException()
        {
            string value = new string('x', 400);

            ImagemapMessageAction action = new ImagemapMessageAction()
            {
                Text = value
            };

            Assert.AreEqual(value, action.Text);
        }

        [TestMethod]
        public void Area_Null_ThrowsException()
        {
            ImagemapMessageAction action = new ImagemapMessageAction();

            ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
            {
                action.Area = null;
            });
        }
    }
}
