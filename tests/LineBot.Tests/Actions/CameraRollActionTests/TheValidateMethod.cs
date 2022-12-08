// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class CameraRollActionTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenLabelIsNull()
            {
                IAction action = new CameraRollAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                IAction action = new CameraRollAction()
                {
                    Label = "Test"
                };

                action.Validate();
            }
        }
    }
}
