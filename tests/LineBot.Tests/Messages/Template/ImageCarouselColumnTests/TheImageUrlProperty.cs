// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageCarouselColumnTests
    {
        [TestClass]
        public class TheTextProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var column = new ImageCarouselColumn
                {
                    ImageUrl = null
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                var column = new ImageCarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The image url should use the https scheme.", () =>
                {
                    column.ImageUrl = new Uri("http://foo.bar");
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var column = new ImageCarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The image url cannot be longer than 1000 characters.", () =>
                {
                    column.ImageUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var column = new ImageCarouselColumn()
                {
                    ImageUrl = value
                };

                Assert.AreEqual(value, column.ImageUrl);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenThumbnailUrlSetAndValueIsMoreThan60Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    column.ThumbnailUrl = new Uri("https://foo.bar/");
                    column.Text = new string('x', 61);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenThumbnailUrlSetAndValueIs60Chars()
            {
                var column = new CarouselColumn()
                {
                    ThumbnailUrl = new Uri("https://foo.bar/"),
                    Text = new string('x', 60)
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTitleSetAndValueIsMoreThan60Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    column.Title = "Test";
                    column.Text = new string('x', 61);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenTitleSetAndValueIs60Chars()
            {
                var template = new CarouselColumn()
                {
                    Title = "Test",
                    Text = new string('x', 60)
                };
            }
        }
    }
}
