// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class DateTimePickerActionTest
    {
        public class TheModeProperty
        {
            [Fact]
            public void ShouldSetDateAndTimeWhenDateTimePickerActionModeIsDateTime()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.DateTime);

                var min = new DateTime(2018, 10, 08, 10, 30, 0);
                action.Min = min;

                Assert.Equal(min, action.Min);
            }

            [Fact]
            public void ShouldSetDateOnlyWhenDateTimePickerActionModeIsDate()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);

                var minDate = new DateTime(2018, 10, 08);
                var minDateTime = new DateTime(2018, 10, 08, 10, 30, 0);
                action.Min = minDateTime;

                Assert.Equal(minDate, action.Min);
            }

            [Fact]
            public void ShouldSetTimeOnlyWhenDateTimePickerActionModeIsTime()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Time);

                var minTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 10, 30, 0);
                var maxTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 11, 00, 0);
                var initialTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 10, 45, 0);

                var minDateTime = new DateTime(2018, 10, 08, 10, 30, 0);
                var maxDateTime = new DateTime(2018, 10, 08, 11, 0, 0);
                var initialDateTime = new DateTime(2018, 10, 08, 10, 45, 0);

                action.Min = minDateTime;
                action.Max = maxDateTime;
                action.Initial = initialDateTime;

                Assert.Equal(minTime, action.Min);
                Assert.Equal(maxTime, action.Max);
                Assert.Equal(initialTime, action.Initial);
            }

            [Fact]
            public void ShouldThrowExceptionWhenMinAndMaxHaveDifferentTimesButModeIsDate()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);

                var min = new DateTime(2018, 10, 8, 10, 0, 0);
                var max = new DateTime(2018, 10, 8, 11, 0, 0);

                ExceptionAssert.Throws<InvalidOperationException>("The max must be greater than the min.", () =>
                {
                    action.Min = min;
                    action.Max = max;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenMinAndMaxHaveDifferentDatesButModeIsTime()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Time);

                var min = new DateTime(2018, 10, 7, 11, 0, 0);
                var max = new DateTime(2018, 10, 8, 11, 0, 0);

                ExceptionAssert.Throws<InvalidOperationException>("The max must be greater than the min.", () =>
                {
                    action.Min = min;
                    action.Max = max;
                });
            }

            [Fact]
            public void ShouldFormatDateTimesAsDateTimesWhenModeIsDateTime()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.DateTime)
                {
                    Label = "Select date",
                    Data = "storeId=12345",
                    Initial = new DateTime(2017, 12, 25, 0, 0, 0),
                    Max = new DateTime(2018, 01, 24, 23, 59, 0),
                    Min = new DateTime(2017, 12, 25, 0, 0, 0)
                };

                var serialized = JsonSerializer.SerializeObject(action);

                Assert.Equal(@"{""mode"":""datetime"",""type"":""datetimepicker"",""data"":""storeId=12345"",""label"":""Select date"",""initial"":""2017-12-25T00:00"",""max"":""2018-01-24T23:59"",""min"":""2017-12-25T00:00""}", serialized);
            }

            [Fact]
            public void ShouldFormatDateTimesAsDatesWhenModeIsDate()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date)
                {
                    Label = "Select date",
                    Data = "storeId=12345",
                    Initial = new DateTime(2017, 12, 25, 0, 0, 0),
                    Max = new DateTime(2018, 01, 24, 23, 59, 0),
                    Min = new DateTime(2017, 12, 25, 0, 0, 0)
                };

                var serialized = JsonSerializer.SerializeObject(action);

                Assert.Equal(@"{""mode"":""date"",""type"":""datetimepicker"",""data"":""storeId=12345"",""label"":""Select date"",""initial"":""2017-12-25"",""max"":""2018-01-24"",""min"":""2017-12-25""}", serialized);
            }

            [Fact]
            public void ShouldFormatDateTimesAsTimesWhenModeIsTime()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Time)
                {
                    Label = "Select date",
                    Data = "storeId=12345",
                    Initial = new DateTime(2017, 12, 25, 14, 0, 0),
                    Max = new DateTime(2018, 01, 24, 16, 0, 0),
                    Min = new DateTime(2017, 12, 25, 13, 0, 0)
                };

                var serialized = JsonSerializer.SerializeObject(action);

                Assert.Equal(@"{""mode"":""time"",""type"":""datetimepicker"",""data"":""storeId=12345"",""label"":""Select date"",""initial"":""14:00"",""max"":""16:00"",""min"":""13:00""}", serialized);
            }
        }
    }
}
