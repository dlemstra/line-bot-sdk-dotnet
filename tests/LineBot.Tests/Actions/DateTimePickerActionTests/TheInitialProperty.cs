// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;
using Xunit;

namespace Line.Tests
{
    public partial class DateTimePickerActionTest
    {
        public class TheInitialProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenInitialIsGreaterThanMax()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date)
                {
                    Min = new DateTime(2018, 10, 8)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The initial must be between the min and the max.", () =>
                {
                    action.Initial = new DateTime(2018, 10, 7);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenInitialIsLessThanMin()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date)
                {
                    Max = new DateTime(2018, 10, 8)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The initial must be between the min and the max.", () =>
                {
                    action.Initial = new DateTime(2018, 10, 9);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenInitialIsBetweenMinAndMax()
            {
                var min = new DateTime(2018, 10, 7);
                var initial = new DateTime(2018, 10, 8);
                var max = new DateTime(2018, 10, 9);

                var action = new DateTimePickerAction(DateTimePickerMode.Date)
                {
                    Min = min,
                    Initial = initial,
                    Max = max
                };

                Assert.Equal(min, action.Min);
                Assert.Equal(initial, action.Initial);
                Assert.Equal(max, action.Max);
            }

            [Fact]
            public void ShouldBeAdjustedWhenDeserializing()
            {
                var jsonData = @"{""type"":""datetimepicker"",""label"":""Foo"",""data"":""Bar"",""mode"":""time"",""initial"":""2018-10-08T10:30"",""max"":""2018-10-08T11:00"",""min"":""2018-10-08T10:00""}";
                var action = JsonConvert.DeserializeObject<DateTimePickerAction>(jsonData);

                Assert.Equal(action.Initial, new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 10, 30, 0));
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

                action.Initial = new DateTime(2018, 10, 11, 10, 30, 0);

                Assert.Equal(action.Initial, new DateTime(2018, 10, 11));
            }
        }
    }
}
