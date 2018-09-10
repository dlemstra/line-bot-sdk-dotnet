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
    public partial class RichMenuBoundsTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsRichMenuBounds()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Width = 1,
                    Height = 1
                };

                var convertedRichMenuBounds = RichMenuBounds.Convert(richMenuBounds);

                Assert.AreSame(richMenuBounds, convertedRichMenuBounds);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWidthIsNotSet()
            {
                var richMenuBounds = new RichMenuBounds();

                ExceptionAssert.Throws<InvalidOperationException>("The width is not set.", () =>
                {
                    RichMenuBounds.Convert(richMenuBounds);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightIsNotSet()
            {
                var richMenuBounds = new RichMenuBounds()
                {
                    Width = 100
                };

                ExceptionAssert.Throws<InvalidOperationException>("The height is not set.", () =>
                {
                    RichMenuBounds.Convert(richMenuBounds);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIRichMenuBoundsToRichMenuBounds()
            {
                var richMenuBounds = new TestRichMenuBounds()
                {
                    X = 1,
                    Y = 2,
                    Width = 3,
                    Height = 4
                };

                var bounds = RichMenuBounds.Convert(richMenuBounds);

                Assert.AreNotSame(bounds, richMenuBounds);
                Assert.AreEqual(1, bounds.X);
                Assert.AreEqual(2, bounds.Y);
                Assert.AreEqual(3, bounds.Width);
                Assert.AreEqual(4, bounds.Height);
            }
        }
    }
}
