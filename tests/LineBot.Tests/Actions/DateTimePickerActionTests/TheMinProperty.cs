// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;
using Xunit;

namespace Line.Tests
{
    public partial class DateTimePickerActionTest
    {
        public class TheMinProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenMinIsGreaterThanMax()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);
                var min = new DateTime(2018, 10, 8);
                var max = new DateTime(2018, 10, 7);

                ExceptionAssert.Throws<InvalidOperationException>("The min must be less than the max.", () =>
                {
                    action.Max = max;
                    action.Min = min;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenMinIsEqualToMax()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);
                var now = new DateTime(2018, 10, 8);

                ExceptionAssert.Throws<InvalidOperationException>("The min must be less than the max.", () =>
                {
                    action.Max = now;
                    action.Min = now;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMinIsLessThanMax()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);
                var min = new DateTime(2018, 10, 7);
                var max = new DateTime(2018, 10, 8);

                action.Max = max;
                action.Min = min;

                Assert.Equal(action.Max, max);
                Assert.Equal(action.Min, min);
            }

            [Fact]
            public void ShouldBeAdjustedWhenDeserializing()
            {
                var jsonData = @"{""type"":""datetimepicker"",""label"":""Foo"",""data"":""Bar"",""mode"":""time"",""initial"":""2018-10-08T10:30"",""max"":""2018-10-08T11:00"",""min"":""2018-10-08T10:00""}";
                var action = JsonConvert.DeserializeObject<DateTimePickerAction>(jsonData);

                Assert.Equal(action.Min, new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 10, 0, 0));
            }

            [Fact]
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

                action.Min = new DateTime(2018, 10, 9, 10, 30, 0);

                Assert.Equal(action.Min, new DateTime(2018, 10, 9));
            }
        }
    }
}
