// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
            [DeploymentItem(JsonDocuments.Actions.Camera)]
            public void ShouldCreateCameraActionWhenTypeIsCamera()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Camera);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as CameraAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Camera", action.Label);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.Actions.CameraRoll)]
            public void ShouldCreateCameraActionWhenTypeIsCameraRoll()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.CameraRoll);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as CameraRollAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Camera Roll", action.Label);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.Actions.DateTimePicker)]
            public void ShouldCreateDateTimePickerActionWhenTypeIsDateTimePicker()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.DateTimePicker);

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
            [DeploymentItem(JsonDocuments.Actions.Location)]
            public void ShouldCreateCameraActionWhenTypeIsLocation()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Location);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as LocationAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Location", action.Label);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.Actions.Message)]
            public void ShouldCreateMessageActionWhenTypeIsMessage()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Message);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as MessageAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Yes", action.Label);
                Assert.AreEqual("No", action.Text);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.Actions.Postback)]
            public void ShouldCreatePostbackActionWhenTypeIsPostback()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Postback);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as PostbackAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("Buy", action.Label);
                Assert.AreEqual("action=buy&itemid=111", action.Data);
                Assert.AreEqual("Buy it", action.Text);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.Actions.Uri)]
            public void ShouldCreateUriActionWhenTypeIsUri()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Uri);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as UriAction;

                Assert.IsNotNull(action);
                Assert.AreEqual("View details", action.Label);
                Assert.AreEqual(new Uri("http://example.com/page/222"), action.Url);
            }
        }
    }
}
