// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImageCarouselColumnTests
    {
        public class TheTextProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var column = new ImageCarouselColumn
                {
                    ImageUrl = null
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                var column = new ImageCarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The image url should use the https scheme.", () =>
                {
                    column.ImageUrl = new Uri("http://foo.bar");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var column = new ImageCarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The image url cannot be longer than 1000 characters.", () =>
                {
                    column.ImageUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var column = new ImageCarouselColumn()
                {
                    ImageUrl = value
                };

                Assert.Equal(value, column.ImageUrl);
            }

            [Fact]
            public void ShouldThrowExceptionWhenThumbnailUrlSetAndValueIsMoreThan60Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    column.ThumbnailUrl = new Uri("https://foo.bar/");
                    column.Text = new string('x', 61);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenThumbnailUrlSetAndValueIs60Chars()
            {
                var column = new CarouselColumn()
                {
                    ThumbnailUrl = new Uri("https://foo.bar/"),
                    Text = new string('x', 60)
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenTitleSetAndValueIsMoreThan60Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    column.Title = "Test";
                    column.Text = new string('x', 61);
                });
            }

            [Fact]
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
