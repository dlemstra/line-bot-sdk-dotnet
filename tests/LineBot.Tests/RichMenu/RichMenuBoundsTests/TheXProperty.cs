// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuBoundsTests
    {
        [TestClass]
        public class TheXProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsBiggerThan2500()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The horizontal position cannot be bigger than 2500.", () => { richMenuBounds.X = 2501; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs2500()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    X = 2500
                };

                Assert.AreEqual(2500, richMenuBounds.X);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsLessThan0()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The horizontal position cannot be less than 0.", () => { richMenuBounds.X = -1; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs0()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    X = 0
                };

                Assert.AreEqual(0, richMenuBounds.X);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWidthPlusValueIsBiggerThan2500()
            {
                var richMenuBounds = new RichMenuBounds();
                richMenuBounds.Width = 2300;

                ExceptionAssert.Throws<InvalidOperationException>("The horizontal postion and width will exceed the rich menu's max width.", () =>
                {
                    richMenuBounds.X = 201;
                });
            }

            [TestMethod]
            public void ShouldNothrowExceptionWhenWidthPlusValueIs2500()
            {
                var richMenuBounds = new RichMenuBounds();

                richMenuBounds.Width = 2300;
                richMenuBounds.X = 200;
            }
        }
    }
}