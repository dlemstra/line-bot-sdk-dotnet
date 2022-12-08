// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class PostbackActionTest
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenLabelIsNull()
            {
                IAction action = new PostbackAction()
                {
                    Data = "PostbackData"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                IAction action = new PostbackAction()
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
                IAction action = new PostbackAction()
                {
                    Data = "PostbackData",
                    Label = "PostbackLabel"
                };

                action.Validate();
            }
        }
    }
}
