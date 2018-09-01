// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class RichMenuRequestClassTests
    {
        [TestMethod]
        public void ShouldNotThrowExceptionWhenNameIsNull()
        {
            var richMenu = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The name cannot be null or whitespace.", () =>
            {
                richMenu.Name = null;
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenNameIsWhitespace()
        {
            var richMenu = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The name cannot be null or whitespace.", () =>
            {
                richMenu.Name = " ";
            });
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenNameIsMoreThan300Chars()
        {
            var richMenu = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The name cannot be longer than 300 characters.", () =>
            {
                richMenu.Name = new string('x', 301);
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenNameIs300Chars()
        {
            var value = new string('x', 300);

            var richMenu = new RichMenuRequest
            {
                Name = value
            };

            Assert.AreEqual(value, richMenu.Name);
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenChatBarTextIsNull()
        {
            var richMenu = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be null or whitespace.", () =>
            {
                richMenu.ChatBarText = null;
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenChatBarTextIsWhitespace()
        {
            var richMenu = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be null or whitespace.", () =>
            {
                richMenu.ChatBarText = " ";
            });
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenChatBarTextIsMoreThan14Chars()
        {
            var richMenu = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be longer than 14 characters.", () =>
            {
                richMenu.ChatBarText = new string('x', 15);
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenChatBarTextIs14Chars()
        {
            var value = new string('x', 14);

            var richMenu = new RichMenuRequest
            {
                ChatBarText = value
            };

            Assert.AreEqual(value, richMenu.ChatBarText);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenRichMenuAreasIsNull()
        {
            var richMenu = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The areas cannot be null.", () =>
            {
                richMenu.Areas = null;
            });
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenRichMenuAreasMoreThan20()
        {
            var richMenuArea = new RichMenuArea
            {
                Action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                RichMenuBounds = new RichMenuBounds { Height = 200, Width = 200, X = 100, Y = 0 }
            };

            var value = new RichMenuArea[]
            {
                richMenuArea, richMenuArea, richMenuArea, richMenuArea, richMenuArea,
                richMenuArea, richMenuArea, richMenuArea, richMenuArea, richMenuArea,
                richMenuArea, richMenuArea, richMenuArea, richMenuArea, richMenuArea,
                richMenuArea, richMenuArea, richMenuArea, richMenuArea, richMenuArea,
                richMenuArea
            };

            var richMenu = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The maximum number of areas is 20.", () =>
            {
                richMenu.Areas = value;
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenRichMenuAreasIs20()
        {
            var richMenuArea = new RichMenuArea
            {
                Action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                RichMenuBounds = new RichMenuBounds { Height = 200, Width = 200, X = 100, Y = 0 }
            };
            var value = new RichMenuArea[]
            {
                richMenuArea, richMenuArea, richMenuArea, richMenuArea, richMenuArea,
                richMenuArea, richMenuArea, richMenuArea, richMenuArea, richMenuArea,
                richMenuArea, richMenuArea, richMenuArea, richMenuArea, richMenuArea,
                richMenuArea, richMenuArea, richMenuArea, richMenuArea, richMenuArea
            };

            var richMenu = new RichMenuRequest();
            richMenu.Areas = value;

            Assert.AreEqual(value, richMenu.Areas);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenRichMenuBoundsIsNull()
        {
            var richMenuArea = new RichMenuArea();

            ExceptionAssert.Throws<InvalidOperationException>("The bounds cannot be null.", () =>
            {
                richMenuArea.RichMenuBounds = null;
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenRichMenuBoundsIsNotNull()
        {
            var richMenuBounds = new RichMenuBounds { Height = 200, Width = 200, X = 100, Y = 0 };

            var richMenuArea = new RichMenuArea
            {
                RichMenuBounds = richMenuBounds
            };

            Assert.AreEqual(richMenuBounds, richMenuArea.RichMenuBounds);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenActionIsNull()
        {
            var richMenuArea = new RichMenuArea();

            ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
            {
                richMenuArea.Action = null;
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenActionIsNotNull()
        {
            var action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") };

            var richMenuArea = new RichMenuArea
            {
                Action = action
            };

            Assert.AreEqual(action, richMenuArea.Action);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenWidthIsNot2500()
        {
            var richMenuSize = new RichMenuSize();

            ExceptionAssert.Throws<InvalidOperationException>("The width must be 2500!", () =>
            {
                richMenuSize.Width = 100;
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenWidthIs2500()
        {
            var richMenuSize = new RichMenuSize()
            {
                Width = 2500
            };

            Assert.AreEqual(2500, richMenuSize.Width);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenHeightIsNot1686_843()
        {
            var richMenuSize = new RichMenuSize();

            ExceptionAssert.Throws<InvalidOperationException>("The possible height values: 1686, 843.", () =>
            {
                richMenuSize.Height = 100;
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenHeightIs1686()
        {
            var richMenuSize = new RichMenuSize()
            {
                Height = 1686
            };

            Assert.AreEqual(1686, richMenuSize.Height);
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenHeightIs843()
        {
            var richMenuSize = new RichMenuSize()
            {
                Height = 843
            };

            Assert.AreEqual(843, richMenuSize.Height);
        }
    }
}