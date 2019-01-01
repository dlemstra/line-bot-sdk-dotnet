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