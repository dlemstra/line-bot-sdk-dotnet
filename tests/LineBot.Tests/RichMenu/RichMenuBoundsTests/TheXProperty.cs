// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuBoundsTests
    {
        public class TheXProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsBiggerThan2500()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The horizontal position cannot be bigger than 2500.", () => { richMenuBounds.X = 2501; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs2500()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    X = 2500
                };

                Assert.Equal(2500, richMenuBounds.X);
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsLessThan0()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The horizontal position cannot be less than 0.", () => { richMenuBounds.X = -1; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs0()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    X = 0
                };

                Assert.Equal(0, richMenuBounds.X);
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthPlusValueIsBiggerThan2500()
            {
                var richMenuBounds = new RichMenuBounds();
                richMenuBounds.Width = 2300;

                ExceptionAssert.Throws<InvalidOperationException>("The horizontal postion and width will exceed the rich menu's max width.", () =>
                {
                    richMenuBounds.X = 201;
                });
            }

            [Fact]
            public void ShouldNothrowExceptionWhenWidthPlusValueIs2500()
            {
                var richMenuBounds = new RichMenuBounds();

                richMenuBounds.Width = 2300;
                richMenuBounds.X = 200;
            }
        }
    }
}
