// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapAreaTests
    {
        public class TheYProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueBelowZero()
            {
                var area = new ImagemapArea();

                ExceptionAssert.Throws<InvalidOperationException>("The y position should be at least 0.", () =>
                {
                    area.Y = -1;
                });
            }
        }
    }
}
