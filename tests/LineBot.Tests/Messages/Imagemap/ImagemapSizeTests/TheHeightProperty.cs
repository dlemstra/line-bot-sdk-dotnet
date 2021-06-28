// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapSizeTests
    {
        [TestClass]
        public class TheHeightProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsZero()
            {
                var size = new ImagemapSize();

                ExceptionAssert.Throws<InvalidOperationException>("The height should be at least 1.", () =>
                {
                    size.Height = 0;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNegative()
            {
                var size = new ImagemapSize();

                ExceptionAssert.Throws<InvalidOperationException>("The height should be at least 1.", () =>
                {
                    size.Height = -1;
                });
            }
        }
    }
}
