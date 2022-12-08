// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;
using Xunit;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        public class TheReadJsonMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenTypeIsInteger()
            {
                var converter = new EnumConverter<TestEnum>();

                ExceptionAssert.Throws<InvalidOperationException>("Only string is supported.", () =>
                {
                    JsonConvert.DeserializeObject<TestEnum>("42", converter);
                });
            }

            [Fact]
            public void ShouldReturnTheDefaultWhenValueIsInvalid()
            {
                var converter = new EnumConverter<TestEnum>();

                var value = JsonConvert.DeserializeObject<TestEnum>(@"""invalid""", converter);
                Assert.Equal(TestEnum.Unknown, value);
            }

            [Fact]
            public void ShouldParseString()
            {
                var converter = new EnumConverter<TestEnum>();

                var value = JsonConvert.DeserializeObject<TestEnum>(@"""Test""", converter);
                Assert.Equal(TestEnum.Test, value);
            }

            [Fact]
            public void ShouldIgnoreCasingWhenParsingString()
            {
                var converter = new EnumConverter<TestEnum>();

                var value = JsonConvert.DeserializeObject<TestEnum>(@"""test""", converter);
                Assert.Equal(TestEnum.Test, value);
            }
        }
    }
}
