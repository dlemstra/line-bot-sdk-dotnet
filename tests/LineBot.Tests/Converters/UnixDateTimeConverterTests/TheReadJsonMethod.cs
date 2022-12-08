// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;
using Xunit;

namespace Line.Tests
{
    public partial class UnixDateTimeConverterTests
    {
        public class TheReadJsonMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenTypeIsNotInteger()
            {
                var converter = new UnixDateTimeConverter();

                ExceptionAssert.Throws<InvalidOperationException>("Only integer is supported.", () =>
                {
                    JsonConvert.DeserializeObject<DateTime>(@"""1234567890""", converter);
                });
            }

            [Fact]
            public void ShouldReturnEpochUtcWhenValueIsZero()
            {
                var converter = new UnixDateTimeConverter();

                var value = JsonConvert.DeserializeObject<DateTime>("0", converter);

                var epoch = new DateTime(1970, 1, 1, 0, 0, 0);
                Assert.Equal(epoch, value);
                Assert.Equal(DateTimeKind.Utc, value.Kind);
            }

            [Fact]
            public void ShouldReturnEpochUtcPlusTwoSecondsWhenValueIs2000()
            {
                var converter = new UnixDateTimeConverter();

                var value = JsonConvert.DeserializeObject<DateTime>("2000", converter);

                var epoch = new DateTime(1970, 1, 1, 0, 0, 2);
                Assert.Equal(epoch, value);
                Assert.Equal(DateTimeKind.Utc, value.Kind);
            }
        }
    }
}
