// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        [TestClass]
        public class TheImageBackgroundColorProperty
        {
            [TestMethod]
            public void ShouldThowExceptionWhenValueIsNull()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color should be 7 characters long.", () =>
                {
                    template.ImageBackgroundColor = null;
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsToLong()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color should be 7 characters long.", () =>
                {
                    template.ImageBackgroundColor = "#FFFFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueNotStartsWithNumberSign()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color should start with #.", () =>
                {
                    template.ImageBackgroundColor = "FFFFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#@FFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid2()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#GFFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid3()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#`FFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid4()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#gFFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid5()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#/FFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid6()
            {
                CarouselColumn template = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#:FFFFF";
                });
            }

            [TestMethod]
            public void ShouldUppercaseTheValue()
            {
                CarouselColumn template = new CarouselColumn
                {
                    ImageBackgroundColor = "#ff00FF"
                };

                Assert.AreEqual("#FF00FF", template.ImageBackgroundColor);
            }
        }
    }
}
