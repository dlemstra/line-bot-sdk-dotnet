// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapSizeTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenWidthIsNotSet()
            {
                var size = new ImagemapSize()
                {
                    Height = 1
                };

                ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
                {
                    size.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNotSet()
            {
                var size = new ImagemapSize()
                {
                    Width = 1
                };

                ExceptionAssert.Throws<InvalidOperationException>("The height should be at least 1.", () =>
                {
                    size.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                var size = new ImagemapSize()
                {
                    Height = 1,
                    Width = 1
                };

                size.Validate();
            }
        }
    }
}
