// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuBoundsTests
    {
        public class TheHeightProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsBiggerThan1686()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The height cannot be bigger than 1686.", () => { richMenuBounds.Height = 1687; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs1686()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Height = 1686
                };

                Assert.Equal(1686, richMenuBounds.Height);
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsLessThan1()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The height cannot be less than 1.", () => { richMenuBounds.Height = 0; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs1()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Height = 1
                };

                Assert.Equal(1, richMenuBounds.Height);
            }

            [Fact]
            public void ShouldThrowExceptionWhenValuePlusYIsBiggerThan1686()
            {
                var richMenuBounds = new RichMenuBounds();
                richMenuBounds.Y = 200;

                ExceptionAssert.Throws<InvalidOperationException>("The vertical postion and height will exceed the rich menu's max height.", () =>
                {
                    richMenuBounds.Height = 1487;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValuePlusYIs1686()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Y = 200,
                    Height = 1486
                };

                Assert.Equal(200, richMenuBounds.Y);
                Assert.Equal(1486, richMenuBounds.Height);
            }
        }
    }
}
