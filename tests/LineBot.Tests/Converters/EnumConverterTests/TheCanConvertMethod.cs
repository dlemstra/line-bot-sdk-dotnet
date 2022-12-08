// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        public class TheCanConvertMethod
        {
            [Fact]
            public void ShouldReturnTrueWhenTypeIsEnum()
            {
                var converter = new EnumConverter<TestEnum>();

                Assert.True(converter.CanConvert(typeof(TestEnum)));
            }

            [Fact]
            public void ShouldReturnTrueWhenTypeIsNullableEnum()
            {
                var converter = new EnumConverter<TestEnum>();

                Assert.False(converter.CanConvert(typeof(TestEnum?)));
            }
        }
    }
}
