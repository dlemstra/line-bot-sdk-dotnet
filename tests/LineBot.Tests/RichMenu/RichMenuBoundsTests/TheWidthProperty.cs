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
        public class TheWidthProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsBiggerThan2500()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The width cannot be bigger than 2500.", () => { richMenuBounds.Width = 2501; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs2500()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Width = 2500
                };

                Assert.AreEqual(2500, richMenuBounds.Width);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsLessThan1()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The width cannot be less than 1.", () => { richMenuBounds.Width = 0; });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs1()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Width = 1
                };

                Assert.AreEqual(1, richMenuBounds.Width);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValuePlusXIsBiggerThan2500()
            {
                var richMenuBounds = new RichMenuBounds();
                richMenuBounds.X = 200;

                ExceptionAssert.Throws<InvalidOperationException>("The horizontal postion and width will exceed the rich menu's max width.", () =>
                {
                    richMenuBounds.Width = 2301;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValuePlusXIs2500()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    X = 200,
                    Width = 2300
                };

                Assert.AreEqual(200, richMenuBounds.X);
                Assert.AreEqual(2300, richMenuBounds.Width);
            }
        }
    }
}