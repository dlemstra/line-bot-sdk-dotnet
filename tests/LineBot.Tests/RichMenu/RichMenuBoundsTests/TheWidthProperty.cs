// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuBoundsTests
    {
        public class TheWidthProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsBiggerThan2500()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The width cannot be bigger than 2500.", () => { richMenuBounds.Width = 2501; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs2500()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Width = 2500
                };

                Assert.Equal(2500, richMenuBounds.Width);
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsLessThan1()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The width cannot be less than 1.", () => { richMenuBounds.Width = 0; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs1()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Width = 1
                };

                Assert.Equal(1, richMenuBounds.Width);
            }

            [Fact]
            public void ShouldThrowExceptionWhenValuePlusXIsBiggerThan2500()
            {
                var richMenuBounds = new RichMenuBounds();
                richMenuBounds.X = 200;

                ExceptionAssert.Throws<InvalidOperationException>("The horizontal postion and width will exceed the rich menu's max width.", () =>
                {
                    richMenuBounds.Width = 2301;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValuePlusXIs2500()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    X = 200,
                    Width = 2300
                };

                Assert.Equal(200, richMenuBounds.X);
                Assert.Equal(2300, richMenuBounds.Width);
            }
        }
    }
}
