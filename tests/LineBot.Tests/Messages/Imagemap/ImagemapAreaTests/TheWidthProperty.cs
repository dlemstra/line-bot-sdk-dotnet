// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapSizeTests
    {
        [TestClass]
        public class ImagemapAreaTests
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsZero()
            {
                var area = new ImagemapArea();

                ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
                {
                    area.Width = 0;
                });
            }

            [TestMethod]
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
