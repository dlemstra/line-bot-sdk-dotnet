// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TemplateMessageTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenAlternativeTextIsNull()
            {
                ISendMessage message = new TemplateMessage()
                {
                    Template = new ButtonsTemplate()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTemplateIsNull()
            {
                ISendMessage message = new TemplateMessage()
                {
                    AlternativeText = "AlternativeText"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The template cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTemplateIsInvalid()
            {
                ISendMessage message = new TemplateMessage()
                {
                    AlternativeText = "AlternativeText",
                    Template = new ButtonsTemplate()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ISendMessage message = new TemplateMessage()
                {
                    AlternativeText = "AlternativeText",
                    Template = new ButtonsTemplate()
                    {
                        Text = "Foo",
                        Actions = new[] { new MessageAction() { Label = "Foo", Text = "Bar" } }
                    }
                };

                message.Validate();
            }
        }
    }
}
