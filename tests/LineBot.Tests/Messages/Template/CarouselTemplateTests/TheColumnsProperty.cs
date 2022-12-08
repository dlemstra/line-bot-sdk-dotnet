// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class CarouselTemplateTests
    {
        public class TheColumnsProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var template = new CarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    template.Columns = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var template = new CarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of columns is 1.", () =>
                {
                    template.Columns = new CarouselColumn[] { };
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueContainsMoreThan10Items()
            {
                var template = new CarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of columns is 10.", () =>
                {
                    template.Columns = new CarouselColumn[]
                    {
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn()
                    };
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueContains10Items()
            {
                var template = new CarouselTemplate()
                {
                    Columns = new CarouselColumn[]
                    {
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn(),
                        new CarouselColumn()
                    }
                };
            }
        }
    }
}
