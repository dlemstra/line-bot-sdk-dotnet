// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class UnixDateTimeConverterTests
    {
        [TestClass]
        public class TheCanConvertMethod
        {
            [TestMethod]
            public void ShouldReturnTrueWhenTypeIsDateTime()
            {
                UnixDateTimeConverter converter = new UnixDateTimeConverter();

                Assert.IsTrue(converter.CanConvert(typeof(DateTime)));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenTypeIsNullableDateTime()
            {
                UnixDateTimeConverter converter = new UnixDateTimeConverter();

                Assert.IsFalse(converter.CanConvert(typeof(DateTime?)));
            }
        }
    }
}
