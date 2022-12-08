// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class UnixDateTimeConverterTests
    {
        public class TheCanConvertMethod
        {
            [Fact]
            public void ShouldReturnTrueWhenTypeIsDateTime()
            {
                var converter = new UnixDateTimeConverter();

                Assert.True(converter.CanConvert(typeof(DateTime)));
            }

            [Fact]
            public void ShouldReturnTrueWhenTypeIsNullableDateTime()
            {
                var converter = new UnixDateTimeConverter();

                Assert.False(converter.CanConvert(typeof(DateTime?)));
            }
        }
    }
}
