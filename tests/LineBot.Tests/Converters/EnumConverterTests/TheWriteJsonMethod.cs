// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;
using Xunit;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        public class TheWriteJsonMethod
        {
            [Fact]
            public void ShouldWriteValueInLowerCase()
            {
                var converter = new EnumConverter<TestEnum>();

                var value = JsonConvert.SerializeObject(TestEnum.Test, converter);
                Assert.Equal(@"""test""", value);
            }
        }
    }
}
