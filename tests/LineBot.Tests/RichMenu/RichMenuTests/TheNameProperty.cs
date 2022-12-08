// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuTests
    {
        public class TheNameProperty
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The name cannot be null or whitespace.", () =>
                {
                    richMenu.Name = null;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsWhitespace()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The name cannot be null or whitespace.", () =>
                {
                    richMenu.Name = " ";
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan300Chars()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The name cannot be longer than 300 characters.", () =>
                {
                    richMenu.Name = new string('x', 301);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs300Chars()
            {
                var value = new string('x', 300);

                var richMenu = new RichMenu
                {
                    Name = value
                };

                Assert.Equal(value, richMenu.Name);
            }
        }
    }
}
