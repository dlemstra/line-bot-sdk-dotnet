// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        public class TheTextProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    column.Text = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    column.Text = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan120Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 120 characters.", () =>
                {
                    column.Text = new string('x', 121);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs120Chars()
            {
                var value = new string('x', 120);

                var column = new CarouselColumn()
                {
                    Text = value
                };

                Assert.Equal(value, column.Text);
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
