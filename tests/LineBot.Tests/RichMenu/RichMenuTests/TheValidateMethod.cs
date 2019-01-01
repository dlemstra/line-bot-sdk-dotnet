// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenAreasIsNull()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The areas cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAreasHasNullValue()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new RichMenuArea[1],
                    ChatBarText = "foobar",
                    Name = "barfoo",
                    Size = new RichMenuSize()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The rich menu area should not be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAreasIsInvalid()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new[] { new RichMenuArea() },
                    ChatBarText = "foobar",
                    Name = "barfoo",
                    Size = new RichMenuSize()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenChatBarTextIsNull()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new RichMenuArea[1]
                };

                ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new RichMenuArea[1],
                    ChatBarText = "foobar"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The name cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSizeIsNull()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new RichMenuArea[1],
                    ChatBarText = "foobar",
                    Name = "barfoo"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The size cannot be null.", () =>
                {
                    richMenu.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSizeIsInvalid()
            {
                var richMenu = new RichMenu()
                {
                    Areas = new[]
                    {
                        new RichMenuArea()
                        {
                            Action = new MessageAction() { Label = "Foo", Text = "Bar" },
                            Bounds = new RichMenuBounds(1, 2, 3, 4)
                        }
                    },
                    ChatBarText = "foobar",
                    Name = "barfoo",
                    Size = new RichMenuSize()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The height is not set.", () =>
                {
                    richMenu.Validate();
                });
            }
        }
    }
}