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
    public partial class TheRichMenuSizeTests
    {
        [TestClass]
        public partial class TheHeightProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenHeightIsNot1686_843()
            {
                var richMenuSize = new RichMenuSize();

                ExceptionAssert.Throws<InvalidOperationException>("The possible height values: 1686, 843.", () => { richMenuSize.Height = 100; });
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
}