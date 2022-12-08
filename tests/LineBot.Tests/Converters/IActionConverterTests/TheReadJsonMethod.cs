// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using Newtonsoft.Json;
using Xunit;

namespace Line.Tests
{
    public partial class IActionConverterTests
    {
        public class TheReadJsonMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenTypeIsInvalid()
            {
                var converter = new IActionConverter();

                ExceptionAssert.Throws<JsonReaderException>("Error reading JObject from JsonReader.", () =>
                {
                    JsonConvert.DeserializeObject<TestAction>("42", converter);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionIsInvalid()
            {
                var converter = new IActionConverter();

                ExceptionAssert.Throws<InvalidOperationException>("The type property is missing.", () =>
                {
                    JsonConvert.DeserializeObject<TestAction>("{}", converter);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionIsNotSupported()
            {
                var converter = new IActionConverter();

                ExceptionAssert.Throws<NotSupportedException>("Unknown type 'foobar'.", () =>
                {
                    JsonConvert.DeserializeObject<TestAction>(@"{""type"" : ""foobar""}", converter);
                });
            }

            [Fact]
            public void ShouldCreateCameraActionWhenTypeIsCamera()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Camera);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as CameraAction;

                Assert.NotNull(action);
                Assert.Equal("Camera", action.Label);
            }

            [Fact]
            public void ShouldCreateCameraActionWhenTypeIsCameraRoll()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.CameraRoll);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as CameraRollAction;

                Assert.NotNull(action);
                Assert.Equal("Camera Roll", action.Label);
            }

            [Fact]
            public void ShouldCreateDateTimePickerActionWhenTypeIsDateTimePicker()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.DateTimePicker);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as DateTimePickerAction;

                Assert.NotNull(action);
                Assert.Equal("Select date", action.Label);
                Assert.Equal("storeId=12345", action.Data);
                Assert.Equal(DateTimePickerMode.DateTime, action.Mode);
                Assert.Equal(new DateTime(2017, 12, 25, 0, 0, 0), action.Initial);
                Assert.Equal(new DateTime(2018, 1, 24, 23, 59, 0), action.Max);
                Assert.Equal(new DateTime(2017, 12, 23, 0, 0, 0), action.Min);
            }

            [Fact]
            public void ShouldCreateCameraActionWhenTypeIsLocation()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Location);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as LocationAction;

                Assert.NotNull(action);
                Assert.Equal("Location", action.Label);
            }

            [Fact]
            public void ShouldCreateMessageActionWhenTypeIsMessage()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Message);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as MessageAction;

                Assert.NotNull(action);
                Assert.Equal("Yes", action.Label);
                Assert.Equal("No", action.Text);
            }

            [Fact]
            public void ShouldCreatePostbackActionWhenTypeIsPostback()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Postback);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as PostbackAction;

                Assert.NotNull(action);
                Assert.Equal("Buy", action.Label);
                Assert.Equal("action=buy&itemid=111", action.Data);
                Assert.Equal("Buy it", action.Text);
            }

            [Fact]
            public void ShouldCreateUriActionWhenTypeIsUri()
            {
                var converter = new IActionConverter();

                var data = File.ReadAllText(JsonDocuments.Actions.Uri);

                var action = JsonConvert.DeserializeObject<IAction>(data, converter) as UriAction;

                Assert.NotNull(action);
                Assert.Equal("View details", action.Label);
                Assert.Equal(new Uri("http://example.com/page/222"), action.Url);
            }
        }
    }
}
