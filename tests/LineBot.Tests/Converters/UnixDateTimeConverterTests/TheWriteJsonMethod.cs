// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        public class UnixDateTimeConverterTests
        {
            [Fact]
            public void ShouldThrowException()
            {
                var converter = new UnixDateTimeConverter();

                ExceptionAssert.Throws<NotSupportedException>(() =>
                {
                    converter.WriteJson(null, null, null);
                });
            }
        }
    }
}
