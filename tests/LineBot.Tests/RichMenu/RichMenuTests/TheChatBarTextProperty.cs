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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuTests
    {
        [TestClass]
        public class TheChatBarTextProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be null or whitespace.", () =>
                {
                    richMenu.ChatBarText = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsWhitespace()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be null or whitespace.", () =>
                {
                    richMenu.ChatBarText = StringTools.Whitespace;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan14Chars()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The chat bar text cannot be longer than 14 characters.", () =>
                {
                    richMenu.ChatBarText = new string('x', 15);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs14Chars()
            {
                var value = new string('x', 14);

                var richMenu = new RichMenu
                {
                    ChatBarText = value
                };

                Assert.AreEqual(value, richMenu.ChatBarText);
            }
        }
    }
}