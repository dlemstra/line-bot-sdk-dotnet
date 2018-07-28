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
    public partial class TheThumbnailUrlProperty
    {
        [TestClass]
        public class TheTitleProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenSetToNull()
            {
                var column = new CarouselColumn
                {
                    Title = null
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan40Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 40 characters.", () =>
                {
                    column.Title = new string('x', 41);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs40Chars()
            {
                var value = new string('x', 40);

                var column = new CarouselColumn()
                {
                    Title = value
                };

                Assert.AreEqual(value, column.Title);
            }
        }
    }
}
