// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TemplateMessageTests
    {
        [TestClass]
        public class TheTemplateProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The template cannot be null.", () =>
                {
                    message.Template = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsButtonsTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ButtonsTemplate()
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsConfirmTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ConfirmTemplate()
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsCarouselTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new CarouselTemplate()
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsImageCarouselTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ImageCarouselTemplate()
                };
            }

            [TestMethod]
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
