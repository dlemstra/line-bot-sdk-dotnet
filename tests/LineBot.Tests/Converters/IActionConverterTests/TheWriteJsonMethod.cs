// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class IActionConverterTests
    {
        public class TheWriteJsonMethod
        {
            [Fact]
            public void ShouldThrowException()
            {
                var converter = new IActionConverter();

                ExceptionAssert.Throws<NotImplementedException>(() => converter.WriteJson(null, null, null));
            }
        }
    }
}
