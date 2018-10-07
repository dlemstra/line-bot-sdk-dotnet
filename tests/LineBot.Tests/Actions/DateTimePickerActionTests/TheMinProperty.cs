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
    public partial class DateTimePickerActionTest
    {
        [TestClass]
        public class TheMinProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenMinIsGreaterThanMax()
            {
                var action = new DateTimePickerAction();

                ExceptionAssert.Throws<InvalidOperationException>("The min must be less than the max.", () =>
                {
                    action.Max = DateTime.Now;
                    action.Min = DateTime.Now.AddDays(1);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMinIsLessThanMax()
            {
                var action = new DateTimePickerAction();
                var now = DateTime.Now;

                action.Max = now;
                action.Min = now.AddDays(-1);

                Assert.AreEqual(action.Max, now);
                Assert.AreEqual(action.Min, now.AddDays(-1));
            }
        }
    }
}
