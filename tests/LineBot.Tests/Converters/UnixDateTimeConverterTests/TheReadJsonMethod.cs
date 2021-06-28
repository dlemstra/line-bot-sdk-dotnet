// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    public partial class UnixDateTimeConverterTests
    {
        [TestClass]
        public class TheReadJsonMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenTypeIsNotInteger()
            {
                UnixDateTimeConverter converter = new UnixDateTimeConverter();

                ExceptionAssert.Throws<InvalidOperationException>("Only integer is supported.", () =>
                {
                    JsonConvert.DeserializeObject<DateTime>(@"""1234567890""", converter);
                });
            }

            [TestMethod]
            public void ShouldReturnEpochUtcWhenValueIsZero()
            {
                UnixDateTimeConverter converter = new UnixDateTimeConverter();

                DateTime value = JsonConvert.DeserializeObject<DateTime>("0", converter);

                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
                Assert.AreEqual(epoch, value);
                Assert.AreEqual(DateTimeKind.Utc, value.Kind);
            }

            [TestMethod]
            public void ShouldReturnEpochUtcPlusTwoSecondsWhenValueIs2000()
            {
                UnixDateTimeConverter converter = new UnixDateTimeConverter();

                DateTime value = JsonConvert.DeserializeObject<DateTime>("2000", converter);

                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 2);
                Assert.AreEqual(epoch, value);
                Assert.AreEqual(DateTimeKind.Utc, value.Kind);
            }
        }
    }
}
