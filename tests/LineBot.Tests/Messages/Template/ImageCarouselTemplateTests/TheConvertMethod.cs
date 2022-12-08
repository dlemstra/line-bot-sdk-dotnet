// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImageCarouselTemplateTests
    {
        public class TheConvertMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenColumnsIsNull()
            {
                ITemplate template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [Fact]
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
