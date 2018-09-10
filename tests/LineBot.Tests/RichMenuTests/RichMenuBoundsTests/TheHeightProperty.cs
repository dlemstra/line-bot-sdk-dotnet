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
    public partial class TheRichMenuBoundsTests
    {
        [TestClass]
        public class TheHeightProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenHeightIsBiggerThan1686()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The height cannot be bigger than 1686.", () => { richMenuBounds.Height = 1687; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenheightIs1686()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Height = 1686
                };

                Assert.AreEqual(1686, richMenuBounds.Height);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightIsLessThan1()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The height cannot be less than 1.", () => { richMenuBounds.Height = 0; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenheightIs1()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Height = 1
                };

                Assert.AreEqual(1, richMenuBounds.Height);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightPlusYIsBiggerThan1686()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The vertical postion and height will exceed the rich menu's max height.", () =>
                {
                    richMenuBounds.Y = 200;
                    richMenuBounds.Height = 1487;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenHeightPlusYIs1686()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Y = 200,
                    Height = 1486
                };

                Assert.AreEqual(200, richMenuBounds.Y);
                Assert.AreEqual(1486, richMenuBounds.Height);
            }
        }
    }
}