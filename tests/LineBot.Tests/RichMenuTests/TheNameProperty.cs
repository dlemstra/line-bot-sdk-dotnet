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
        public class TheNameProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenNameIsNull()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The name cannot be null or whitespace.", () =>
                {
                    richMenu.Name = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenNameIsWhitespace()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The name cannot be null or whitespace.", () =>
                {
                    richMenu.Name = " ";
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenNameIsMoreThan300Chars()
            {
                var richMenu = new RichMenu();

                ExceptionAssert.Throws<InvalidOperationException>("The name cannot be longer than 300 characters.", () =>
                {
                    richMenu.Name = new string('x', 301);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenNameIs300Chars()
            {
                var value = new string('x', 300);

                var richMenu = new RichMenu
                {
                    Name = value
                };

                Assert.AreEqual(value, richMenu.Name);
            }
        }
    }
}