// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class TextMessageTests
    {
        public class TheTextProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new TextMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    message.Text = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new TextMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    message.Text = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan2000Chars()
            {
                var message = new TextMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 2000 characters.", () =>
                {
                    message.Text = new string('x', 2001);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs2000Chars()
            {
                var value = new string('x', 2000);

                var message = new TextMessage()
                {
                    Text = value
                };

                Assert.Equal(value, message.Text);
            }
        }
    }
}
