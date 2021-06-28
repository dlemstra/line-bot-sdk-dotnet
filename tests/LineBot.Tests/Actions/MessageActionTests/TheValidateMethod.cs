// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class MessageActionTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
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

            [TestMethod]
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

            [TestMethod]
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
