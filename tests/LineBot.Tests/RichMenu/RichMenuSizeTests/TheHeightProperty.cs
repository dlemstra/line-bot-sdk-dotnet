// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TheRichMenuSizeTests
    {
        [TestClass]
        public partial class TheHeightProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNot1686_843()
            {
                var richMenuSize = new RichMenuSize();

                ExceptionAssert.Throws<InvalidOperationException>("The possible height values are: 1686, 843.", () => { richMenuSize.Height = 100; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs1686()
            {
                var richMenuSize = new RichMenuSize()
                {
                    Height = 1686
                };

                Assert.AreEqual(1686, richMenuSize.Height);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs843()
            {
                var richMenuSize = new RichMenuSize()
                {
                    Height = 843
                };

                Assert.AreEqual(843, richMenuSize.Height);
            }
        }
    }
}