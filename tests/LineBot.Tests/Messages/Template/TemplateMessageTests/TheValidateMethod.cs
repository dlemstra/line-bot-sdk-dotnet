// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class TemplateMessageTests
    {
        public class TheValidateMethod
        {
            [Fact]
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

            [Fact]
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

            [Fact]
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

            [Fact]
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
