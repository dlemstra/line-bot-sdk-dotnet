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
        public class TheMaxProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenMaxIsLessThanMax()
            {
                var action = new DateTimePickerAction();

                ExceptionAssert.Throws<InvalidOperationException>("The max must be greater than the min.", () =>
                {
                    action.Min = DateTime.Now.AddDays(1);
                    action.Max = DateTime.Now;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMaxIsGreaterThanMin()
            {
                var action = new DateTimePickerAction();
                var now = DateTime.Now;

                action.Min = now.AddDays(-1);
                action.Max = now;

                Assert.AreEqual(action.Min, now.AddDays(-1));
                Assert.AreEqual(action.Max, now);
            }
        }
    }
}
