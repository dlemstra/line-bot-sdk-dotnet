// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapMessageActionTests
    {
        public class TheTextProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var action = new ImagemapMessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    action.Text = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var action = new ImagemapMessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    action.Text = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan400Chars()
            {
                var action = new ImagemapMessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 400 characters.", () =>
                {
                    action.Text = new string('x', 401);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs400Chars()
            {
                var value = new string('x', 400);

                var action = new ImagemapMessageAction()
                {
                    Text = value
                };

                Assert.Equal(value, action.Text);
            }
        }
    }
}
