// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class DateTimePickerActionTest
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenLabelIsNull()
            {
                IAction action = new DateTimePickerAction(DateTimePickerMode.Date);

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenDataIsNull()
            {
                IAction action = new DateTimePickerAction(DateTimePickerMode.Date)
                {
                    Label = "PostbackLabel"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The data cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                IAction action = new DateTimePickerAction(DateTimePickerMode.Date)
                {
                    Label = "Foo",
                    Data = "Bar"
                };

                action.Validate();
            }
        }
    }
}
