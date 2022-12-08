// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class TheRichMenuSizeTests
    {
        public partial class TheHeightProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNot1686_843()
            {
                var richMenuSize = new RichMenuSize();

                ExceptionAssert.Throws<InvalidOperationException>("The possible height values are: 1686, 843.", () => { richMenuSize.Height = 100; });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs1686()
            {
                var richMenuSize = new RichMenuSize()
                {
                    Height = 1686
                };

                Assert.Equal(1686, richMenuSize.Height);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs843()
            {
                var richMenuSize = new RichMenuSize()
                {
                    Height = 843
                };

                Assert.Equal(843, richMenuSize.Height);
            }
        }
    }
}
