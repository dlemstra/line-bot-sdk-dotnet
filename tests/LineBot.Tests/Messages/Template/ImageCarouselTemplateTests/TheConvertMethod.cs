// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageCarouselTemplateTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsIsNull()
            {
                ITemplate template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsIsInvalid()
            {
                ITemplate template = new ImageCarouselTemplate()
                {
                    Columns = new[]
                    {
                        new ImageCarouselColumn()
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The image url cannot be null.", () =>
                {
                    template.Validate();
                });
            }
        }
    }
}
