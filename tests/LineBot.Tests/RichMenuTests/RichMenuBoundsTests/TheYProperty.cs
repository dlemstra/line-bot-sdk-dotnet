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
        public class TheYProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenYIsBiggerThan1686()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The vertical position cannot be bigger than 1686.", () => { richMenuBounds.Y = 1687; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenYIs1686()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Y = 1686
                };

                Assert.AreEqual(1686, richMenuBounds.Y);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenYIsLessThan0()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The vertical position cannot be less than 0.", () => { richMenuBounds.Y = -1; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenYIs0()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Y = 0
                };

                Assert.AreEqual(0, richMenuBounds.Y);
            }
        }
    }
}