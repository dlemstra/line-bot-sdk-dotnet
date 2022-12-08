// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class TheThumbnailUrlProperty
    {
        public class TheTitleProperty
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var column = new CarouselColumn
                {
                    Title = null
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan40Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 40 characters.", () =>
                {
                    column.Title = new string('x', 41);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs40Chars()
            {
                var value = new string('x', 40);

                var column = new CarouselColumn()
                {
                    Title = value
                };

                Assert.Equal(value, column.Title);
            }
        }
    }
}
