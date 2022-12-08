// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuAreaTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenActionIsNull()
            {
                var richMenuArea = new RichMenuArea();

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionIsInvalid()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction(),
                    Bounds = new RichMenuBounds()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenBoundsIsNull()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The bounds cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenBoundsIsInvalid()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction() { Label = "Foo", Text = "Bar" },
                    Bounds = new RichMenuBounds()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The width is not set.", () =>
                {
                    richMenuArea.Validate();
                });
            }
        }
    }
}
