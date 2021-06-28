// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
