// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class TemplateMessageTests
    {
        public class TheTemplateProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The template cannot be null.", () =>
                {
                    message.Template = null;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsButtonsTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ButtonsTemplate()
                };
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsConfirmTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ConfirmTemplate()
                };
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsCarouselTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new CarouselTemplate()
                };
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsImageCarouselTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ImageCarouselTemplate()
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsInvalid()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The template type is invalid.", () =>
                {
                    message.Template = new TestTemplate();
                });
            }

            private class TestTemplate : ITemplate
            {
                TemplateType ITemplate.Type
                    => (TemplateType)42;

                public void Validate()
                {
                }
            }
        }
    }
}
