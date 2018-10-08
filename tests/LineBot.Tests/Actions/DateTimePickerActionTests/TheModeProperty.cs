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
        public class TheModeProperty
        {
            [TestMethod]
            public void ShouldSetDateAndTimeWhenDateTimePickerActionModeIsDateTime()
            {
                var action = new DateTimePickerAction
                {
                    Mode = DateTimePickerMode.DateTime
                };

                var min = new DateTime(2018, 10, 08, 10, 30, 0);
                action.Min = min;

                Assert.AreEqual(min, action.Min);
            }

            [TestMethod]
            public void ShouldSetDateOnlyWhenDateTimePickerActionModeIsDate()
            {
                var action = new DateTimePickerAction
                {
                    Mode = DateTimePickerMode.Date
                };

                var minDate = new DateTime(2018, 10, 08);
                var minDateTime = new DateTime(2018, 10, 08, 10, 30, 0);
                action.Min = minDateTime;

                Assert.AreEqual(minDate, action.Min);
            }

            [TestMethod]
            public void ShouldSetTimeOnlyWhenDateTimePickerActionModeIsTime()
            {
                var action = new DateTimePickerAction
                {
                    Mode = DateTimePickerMode.Time
                };

                var minTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 10, 30, 0);
                var maxTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 11, 00, 0);
                var initialTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 10, 45, 0);

                var minDateTime = new DateTime(2018, 10, 08, 10, 30, 0);
                var maxDateTime = new DateTime(2018, 10, 08, 11, 0, 0);
                var initialDateTime = new DateTime(2018, 10, 08, 10, 45, 0);

                action.Min = minDateTime;
                action.Max = maxDateTime;
                action.Initial = initialDateTime;

                Assert.AreEqual(minTime, action.Min);
                Assert.AreEqual(maxTime, action.Max);
                Assert.AreEqual(initialTime, action.Initial);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMinAndMaxHaveDifferentTimesButModeIsDate()
            {
                var action = new DateTimePickerAction
                {
                    Mode = DateTimePickerMode.Date
                };

                var min = new DateTime(2018, 10, 8, 10, 00, 0);
                var max = new DateTime(2018, 10, 8, 11, 00, 0);

                ExceptionAssert.Throws<InvalidOperationException>("The max must be greater than the min.", () =>
                {
                    action.Min = min;
                    action.Max = max;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMinAndMaxHaveDifferentDatesButModeIsTime()
            {
                var action = new DateTimePickerAction
                {
                    Mode = DateTimePickerMode.Time
                };

                var min = new DateTime(2018, 10, 7, 11, 00, 0);
                var max = new DateTime(2018, 10, 8, 11, 00, 0);

                ExceptionAssert.Throws<InvalidOperationException>("The max must be greater than the min.", () =>
                {
                    action.Min = min;
                    action.Max = max;
                });
            }
        }
    }
}