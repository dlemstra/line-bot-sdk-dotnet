// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapAreaTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenWidthIsZero()
            {
                var area = new ImagemapArea()
                {
                    Height = 200
                };

                ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
                {
                    area.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightIsZero()
            {
                var area = new ImagemapArea()
                {
                    Width = 100
                };

                ExceptionAssert.Throws<InvalidOperationException>("The height should be at least 1.", () =>
                {
                    area.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                var area = new ImagemapArea()
                {
                    Height = 200,
                    Width = 100
                };

                area.Validate();
            }
        }
    }
}
