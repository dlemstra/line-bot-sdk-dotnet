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
    [TestClass]
    public class TheSizePropertyTests
    {
        [TestMethod]
        public void ShouldThrowExceptionWhenHeightIsNot1686_843()
        {
            var richMenuRequest = new RichMenuRequest();

            ExceptionAssert.Throws<InvalidOperationException>("The size cannot be null.", () =>
            {
                richMenuRequest.Size = null;
            });
        }

        [TestMethod]
        public void ShouldNotThrowExceptionWhenSizeIsNotNull()
        {
            var size = new RichMenuSize()
            {
                Height = 1686
            };
            var richMenuRequest = new RichMenuRequest();
            richMenuRequest.Size = size;

            Assert.AreEqual(size, richMenuRequest.Size);
        }
    }
}