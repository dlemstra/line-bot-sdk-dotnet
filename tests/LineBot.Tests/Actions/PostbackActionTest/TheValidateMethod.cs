// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class PostbackActionTest
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
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

            [TestMethod]
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

            [TestMethod]
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
