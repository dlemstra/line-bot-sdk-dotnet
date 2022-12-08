// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapSizeTests
    {
        public class ImagemapAreaTests
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsZero()
            {
                var area = new ImagemapArea();

                ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
                {
                    area.Width = 0;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNegative()
            {
                var area = new ImagemapArea();

                ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
                {
                    area.Width = -1;
                });
            }
        }
    }
}
