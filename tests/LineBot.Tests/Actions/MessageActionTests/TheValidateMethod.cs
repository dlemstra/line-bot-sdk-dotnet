// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class MessageActionTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenLabelIsNull()
            {
                IAction action = new MessageAction()
                {
                    Text = "Foo"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                IAction action = new MessageAction()
                {
                    Label = "Test"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                IAction action = new MessageAction()
                {
                    Text = "Foo",
                    Label = "Test"
                };

                action.Validate();
            }
        }
    }
}
