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
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    public partial class IActionConverterTests
    {
        [TestClass]
        public class TheReadJsonMethod
        {
            private const string CameraActionJson = "Converters/IActionConverterTests/CameraAction.json";
            private const string DateTimePickerActionJson = "Converters/IActionConverterTests/DateTimePickerAction.json";
            private const string MessageActionJson = "Converters/IActionConverterTests/MessageAction.json";
            private const string PostbackActionJson = "Converters/IActionConverterTests/PostbackAction.json";
            private const string UriActionJson = "Converters/IActionConverterTests/UriAction.json";

            [TestMethod]
            public void ShouldThrowExceptionWhenTypeIsInvalid()
            {
                var converter = new IActionConverter();

                ExceptionAssert.Throws<JsonReaderException>("Error reading JObject from JsonReader.", () =>
                {
                    JsonConvert.DeserializeObject<TestAction>("42", converter);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsInvalid()
            {
                var converter = new IActionConverter();

                ExceptionAssert.Throws<InvalidOperationException>("The type property is missing.", () =>
                {
                    JsonConvert.DeserializeObject<TestAction>("{}", converter);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsNotSupported()
            {
                var converter = new IActionConverter();

                ExceptionAssert.Throws<NotSupportedException>("Unknown type 'foobar'.", () =>
                {
                    JsonConvert.DeserializeObject<TestAction>(@"{""type"" : ""foobar""}", converter);
                });
            }

            [TestMethod]
            [DeploymentItem(CameraActionJson)]
            public void ShouldCreateCameraActionWhenTypeIsCamera()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(CameraActionJson);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as CameraAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Camera", action.Label);
            }

            [TestMethod]
            [DeploymentItem(DateTimePickerActionJson)]
            public void ShouldCreateDateTimePickerActionWhenTypeIsDateTimePicker()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(DateTimePickerActionJson);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as DateTimePickerAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Select date", action.Label);
                Assert.AreEqual("storeId=12345", action.Data);
                Assert.AreEqual(DateTimePickerMode.DateTime, action.Mode);
                Assert.AreEqual(new DateTime(2017, 12, 25, 0, 0, 0), action.Initial);
                Assert.AreEqual(new DateTime(2018, 1, 24, 23, 59, 0), action.Max);
                Assert.AreEqual(new DateTime(2017, 12, 23, 0, 0, 0), action.Min);
            }

            [TestMethod]
            [DeploymentItem(MessageActionJson)]
            public void ShouldCreateMessageActionWhenTypeIsMessage()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(MessageActionJson);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as MessageAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Yes", action.Label);
                Assert.AreEqual("No", action.Text);
            }

            [TestMethod]
            [DeploymentItem(PostbackActionJson)]
            public void ShouldCreatePostbackActionWhenTypeIsPostback()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(PostbackActionJson);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as PostbackAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Buy", action.Label);
                Assert.AreEqual("action=buy&itemid=111", action.Data);
                Assert.AreEqual("Buy it", action.Text);
            }

            [TestMethod]
            [DeploymentItem(UriActionJson)]
            public void ShouldCreateUriActionWhenTypeIsUri()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(UriActionJson);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as UriAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("View details", action.Label);
                Assert.AreEqual(new Uri("http://example.com/page/222"), action.Url);
            }
        }
    }
}
