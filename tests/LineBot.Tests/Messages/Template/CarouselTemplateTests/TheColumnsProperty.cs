// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class CarouselTemplateTests
    {
        [TestClass]
        public class TheColumnsProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                CarouselTemplate template = new CarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    template.Columns = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                CarouselTemplate template = new CarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of columns is 1.", () =>
                {
                    template.Columns = new CarouselColumn[] { };
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueContainsMoreThan10Items()
            {
                CarouselTemplate template = new CarouselTemplate();

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

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueContains10Items()
            {
                CarouselTemplate template = new CarouselTemplate()
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
