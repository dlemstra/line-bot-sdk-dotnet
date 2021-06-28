// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuTests
    {
        [TestClass]
        public class TheAreasProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The areas cannot be null.", () =>
                {
                    richMenu.Areas = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var richMenuArea = new RichMenuArea
                {
                    Action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                    Bounds = new RichMenuBounds { Height = 200, Width = 200, X = 100, Y = 0 }
                };

                var value = new RichMenuArea[0];

                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of areas is 1.", () =>
                {
                    richMenu.Areas = value;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenLengthOfValueMoreThan20()
            {
                var richMenuArea = new RichMenuArea
                {
                    Action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                    Bounds = new RichMenuBounds { Height = 200, Width = 200, X = 100, Y = 0 }
                };

                var value = Enumerable.Repeat(richMenuArea, 21).ToArray();

                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of areas is 20.", () =>
                {
                    richMenu.Areas = value;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenLengthOfValueIs20()
            {
                var richMenuArea = new RichMenuArea
                {
                    Action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                    Bounds = new RichMenuBounds { Height = 200, Width = 200, X = 100, Y = 0 }
                };

                var value = Enumerable.Repeat(richMenuArea, 20).ToArray();

                var richMenu = new RichMenu();
                richMenu.Areas = value;

                Assert.AreEqual(value, richMenu.Areas);
            }
        }
    }
}