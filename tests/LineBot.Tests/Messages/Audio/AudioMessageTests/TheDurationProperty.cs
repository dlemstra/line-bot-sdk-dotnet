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
    public partial class AudioMessageTests
    {
        [TestClass]
        public class TheDurationProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsZero()
            {
                AudioMessage message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
                {
                    message.Duration = 0;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMinusOne()
            {
                AudioMessage message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
                {
                    message.Duration = -1;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan59999()
            {
                AudioMessage message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration cannot be longer than 1 minute.", () =>
                {
                    message.Duration = 60000;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenUrlIs59999Milliseconds()
            {
                int value = 59999;

                AudioMessage message = new AudioMessage()
                {
                    Duration = value
                };

                Assert.AreEqual(value, message.Duration);
            }
        }
    }
}
