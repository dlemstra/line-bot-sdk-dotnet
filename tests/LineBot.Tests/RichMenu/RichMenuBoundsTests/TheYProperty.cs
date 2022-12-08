// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuBoundsTests
    {
        public class TheYProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsBiggerThan1686()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The vertical position cannot be bigger than 1686.", () => { richMenuBounds.Y = 1687; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs1686()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Y = 1686
                };

                Assert.Equal(1686, richMenuBounds.Y);
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsLessThan0()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The vertical position cannot be less than 0.", () => { richMenuBounds.Y = -1; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs0()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Y = 0
                };

                Assert.Equal(0, richMenuBounds.Y);
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightPlusValueIsBiggerThan1686()
            {
                var richMenuBounds = new RichMenuBounds();
                richMenuBounds.Height = 1487;

                ExceptionAssert.Throws<InvalidOperationException>("The vertical postion and height will exceed the rich menu's max height.", () =>
                {
                    richMenuBounds.Y = 200;
                });
            }

            [Fact]
            public void ShouldNothrowExceptionWhenHeightPlusValueIs2500()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Height = 1486,
                    Y = 200
                };

                Assert.Equal(200, richMenuBounds.Y);
                Assert.Equal(1486, richMenuBounds.Height);
            }
        }
    }
}
