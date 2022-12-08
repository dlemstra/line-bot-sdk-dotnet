// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuAreaTests
    {
        public class TheBoundsProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var richMenuArea = new RichMenuArea();

                ExceptionAssert.Throws<InvalidOperationException>("The bounds cannot be null.", () =>
                {
                    richMenuArea.Bounds = null;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNotNull()
            {
                var richMenuBounds = new RichMenuBounds { Height = 200, Width = 200, X = 100, Y = 0 };

                var richMenuArea = new RichMenuArea
                {
                    Bounds = richMenuBounds
                };

                Assert.Equal(richMenuBounds, richMenuArea.Bounds);
            }
        }
    }
}
