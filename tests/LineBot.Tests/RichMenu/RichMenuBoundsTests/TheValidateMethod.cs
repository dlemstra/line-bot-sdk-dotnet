// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuBoundsTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenWidthIsNotSet()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The width is not set.", () =>
                {
                    richMenuBounds.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNotSet()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Width = 100
                };

                ExceptionAssert.Throws<InvalidOperationException>("The height is not set.", () =>
                {
                    richMenuBounds.Validate();
                });
            }
        }
    }
}
