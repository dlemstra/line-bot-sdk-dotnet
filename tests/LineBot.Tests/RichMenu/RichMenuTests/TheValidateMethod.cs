// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenAreasIsNull()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The areas cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenAreasHasNullValue()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new RichMenuArea[1],
                    ChatBarText = "foobar",
                    Name = "barfoo",
                    Size = new RichMenuSize()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The rich menu area should not be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenAreasIsInvalid()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new[] { new RichMenuArea() },
                    ChatBarText = "foobar",
                    Name = "barfoo",
                    Size = new RichMenuSize()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenChatBarTextIsNull()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new RichMenuArea[1]
                };

                ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new RichMenuArea[1],
                    ChatBarText = "foobar"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The name cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenSizeIsNull()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new RichMenuArea[1],
                    ChatBarText = "foobar",
                    Name = "barfoo"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The size cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenSizeIsInvalid()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new[]
                    {
                        new RichMenuArea()
                        {
                            Action = new MessageAction() { Label = "Foo", Text = "Bar" },
                            Bounds = new RichMenuBounds(1, 2, 3, 4)
                        }
                    },
                    ChatBarText = "foobar",
                    Name = "barfoo",
                    Size = new RichMenuSize()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The height is not set.", () =>
                {
                    richMenu.Validate();
                });
            }
        }
    }
}
