// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuTests
    {
        public class TheChatBarTextProperty
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be null or whitespace.", () =>
                {
                    richMenu.ChatBarText = null;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsWhitespace()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be null or whitespace.", () =>
                {
                    richMenu.ChatBarText = " ";
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan14Chars()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be longer than 14 characters.", () =>
                {
                    richMenu.ChatBarText = new string('x', 15);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs14Chars()
            {
                var value = new string('x', 14);

                var richMenu = new RichMenu
                {
                    ChatBarText = value
                };

                Assert.Equal(value, richMenu.ChatBarText);
            }
        }
    }
}
