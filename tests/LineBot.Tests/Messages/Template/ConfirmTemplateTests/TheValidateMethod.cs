// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ConfirmTemplateTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    OkAction = new MessageAction(),
                    CancelAction = new UriAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenOkActionIsNull()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    CancelAction = new UriAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The ok action cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenOkActionIsInvalid()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new UriAction() { Label = "Foo" },
                    CancelAction = new MessageAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCancelActionIsNull()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The cancel action cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCancelActionIsInvalid()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction() { Label = "Foo", Text = "Bar" },
                    CancelAction = new UriAction() { Label = "Foo" }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ITemplate template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction() { Label = "Foo", Text = "Bar" },
                    CancelAction = new MessageAction() { Label = "Foo", Text = "Bar" }
                };

                template.Validate();
            }
        }
    }
}
