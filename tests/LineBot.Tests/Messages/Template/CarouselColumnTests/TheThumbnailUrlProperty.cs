// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class TheThumbnailUrlProperty
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var column = new CarouselColumn
                {
                    ThumbnailUrl = null
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url should use the https scheme.", () =>
                {
                    column.ThumbnailUrl = new Uri("http://foo.bar");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url cannot be longer than 1000 characters.", () =>
                {
                    column.ThumbnailUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var column = new CarouselColumn()
                {
                    ThumbnailUrl = value
                };

                Assert.Equal(value, column.ThumbnailUrl);
            }
        }
    }
}
