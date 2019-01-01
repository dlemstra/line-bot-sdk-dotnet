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
    public partial class ImagemapMessageActionTests
    {
        [TestClass]
        public class TheLabelProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan50Chars()
            {
                var action = new ImagemapMessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be longer than 50 characters.", () =>
                {
                    action.Label = new string('x', 51);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs50Chars()
            {
                string value = new string('x', 50);

                var action = new ImagemapMessageAction()
                {
                    Label = value
                };

                Assert.AreEqual(value, action.Label);
            }
        }
    }
}
