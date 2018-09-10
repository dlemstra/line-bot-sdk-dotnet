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
    public partial class RichMenuSizeTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsRichMenuSize()
            {
                var richMenuSize = new RichMenuSize()
                {
                    Height = 843
                };

                var convertedRichMenuSize = RichMenuSize.Convert(richMenuSize);

                Assert.AreSame(richMenuSize, convertedRichMenuSize);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightIsNotSet()
            {
                var richMenuSize = new RichMenuSize();

                ExceptionAssert.Throws<InvalidOperationException>("The height is not set.", () =>
                {
                    RichMenuSize.Convert(richMenuSize);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIRichMenuSizeToRichMenuSize()
            {
                var richMenuSize = new TestRichMenuSize()
                {
                    Height = 843
                };

                var size = RichMenuSize.Convert(richMenuSize);

                Assert.AreNotSame(size, richMenuSize);
                Assert.AreEqual(2500, size.Width);
                Assert.AreEqual(843, size.Height);
            }
        }
    }
}
