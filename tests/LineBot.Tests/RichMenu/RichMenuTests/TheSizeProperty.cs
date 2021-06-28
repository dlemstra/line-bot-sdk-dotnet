// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuTests
    {
        [TestClass]
        public class TheSizeProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The size cannot be null.", () =>
                {
                    richMenu.Size = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNotNull()
            {
                var size = new RichMenuSize()
                {
                    Height = 1686
                };
                var richMenu = new RichMenu();
                richMenu.Size = size;

                Assert.AreEqual(size, richMenu.Size);
            }
        }
    }
}