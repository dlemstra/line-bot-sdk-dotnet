// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageCarouselTemplateTests
    {
        [TestClass]
        public class TheColumnsProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                ImageCarouselTemplate template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    template.Columns = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                ImageCarouselTemplate template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of columns is 1.", () =>
                {
                    template.Columns = new ImageCarouselColumn[] { };
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueContainsMoreThan10Items()
            {
                ImageCarouselTemplate template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of columns is 10.", () =>
                {
                    template.Columns = new ImageCarouselColumn[]
                    {
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn()
                    };
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueContains10Items()
            {
                ImageCarouselTemplate template = new ImageCarouselTemplate()
                {
                    Columns = new ImageCarouselColumn[]
                    {
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn()
                    }
                };
            }
        }
    }
}
