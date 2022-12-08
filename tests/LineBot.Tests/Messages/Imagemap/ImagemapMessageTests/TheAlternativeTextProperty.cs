// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapMessageTests
    {
        public class TheAlternativeTextProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
                {
                    message.AlternativeText = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
                {
                    message.AlternativeText = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan400Chars()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be longer than 400 characters.", () =>
                {
                    message.AlternativeText = new string('x', 401);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs400Chars()
            {
                var value = new string('x', 400);

                var message = new ImagemapMessage()
                {
                    AlternativeText = value
                };

                Assert.Equal(value, message.AlternativeText);
            }
        }
    }
}
