// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        public class TheImageBackgroundColorProperty
        {
            [Fact]
            public void ShouldThowExceptionWhenValueIsNull()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color should be 7 characters long.", () =>
                {
                    template.ImageBackgroundColor = null;
                });
            }

            [Fact]
            public void ShouldThowExceptionWhenValueIsToLong()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color should be 7 characters long.", () =>
                {
                    template.ImageBackgroundColor = "#FFFFFFF";
                });
            }

            [Fact]
            public void ShouldThowExceptionWhenValueNotStartsWithNumberSign()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color should start with #.", () =>
                {
                    template.ImageBackgroundColor = "FFFFFFF";
                });
            }

            [Fact]
            public void ShouldThowExceptionWhenValueIsInvalid()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#@FFFFF";
                });
            }

            [Fact]
            public void ShouldThowExceptionWhenValueIsInvalid2()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#GFFFFF";
                });
            }

            [Fact]
            public void ShouldThowExceptionWhenValueIsInvalid3()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#`FFFFF";
                });
            }

            [Fact]
            public void ShouldThowExceptionWhenValueIsInvalid4()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#gFFFFF";
                });
            }

            [Fact]
            public void ShouldThowExceptionWhenValueIsInvalid5()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#/FFFFF";
                });
            }

            [Fact]
            public void ShouldThowExceptionWhenValueIsInvalid6()
            {
                var template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#:FFFFF";
                });
            }

            [Fact]
            public void ShouldUppercaseTheValue()
            {
                var template = new CarouselColumn
                {
                    ImageBackgroundColor = "#ff00FF"
                };

                Assert.Equal("#FF00FF", template.ImageBackgroundColor);
            }
        }
    }
}
