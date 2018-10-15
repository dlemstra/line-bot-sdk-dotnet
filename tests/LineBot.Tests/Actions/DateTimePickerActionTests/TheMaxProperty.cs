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
using Newtonsoft.Json;

namespace Line.Tests
{
    public partial class DateTimePickerActionTest
    {
        [TestClass]
        public class TheMaxProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenMaxIsLessThanMin()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);
                var min = new DateTime(2018, 10, 8);
                var max = new DateTime(2018, 10, 7);

                ExceptionAssert.Throws<InvalidOperationException>("The max must be greater than the min.", () =>
                {
                    action.Min = min;
                    action.Max = max;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMaxIsEqualToMin()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);
                var dt = new DateTime(2018, 10, 8);

                ExceptionAssert.Throws<InvalidOperationException>("The min must be less than the max.", () =>
                {
                    action.Max = dt;
                    action.Min = dt;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMaxIsGreaterThanMin()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);
                var min = new DateTime(2018, 10, 7);
                var max = new DateTime(2018, 10, 8);

                action.Min = min;
                action.Max = max;

                Assert.AreEqual(action.Min, min);
                Assert.AreEqual(action.Max, max);
            }

            [TestMethod]
            public void ShouldBeAdjustedWhenDeserializing()
            {
                var jsonData = @"{""type"":""datetimepicker"",""label"":""Foo"",""data"":""Bar"",""mode"":""time"",""initial"":""2018-10-08T10:30"",""max"":""2018-10-08T11:00"",""min"":""2018-10-08T10:00""}";
                var action = JsonConvert.DeserializeObject<DateTimePickerAction>(jsonData);

                Assert.AreEqual(action.Max, new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 11, 0, 0));
            }

            [TestMethod]
            public void ShouldBeAdjustedWhenSet()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date)
                {
                    Data = "Foo",
                    Label = "Bar",
                    Min = new DateTime(2018, 10, 10),
                    Initial = new DateTime(2018, 10, 11),
                    Max = new DateTime(2018, 10, 13)
                };

                action.Max = new DateTime(2018, 10, 12, 10, 30, 0);

                Assert.AreEqual(action.Max, new DateTime(2018, 10, 12));
            }
        }
    }
}
