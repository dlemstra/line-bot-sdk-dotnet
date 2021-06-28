// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        [TestClass]
        public class TheReadJsonMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenTypeIsInteger()
            {
                EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

                ExceptionAssert.Throws<InvalidOperationException>("Only string is supported.", () =>
                {
                    JsonConvert.DeserializeObject<TestEnum>("42", converter);
                });
            }

            [TestMethod]
            public void ShouldReturnTheDefaultWhenValueIsInvalid()
            {
                EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

                TestEnum value = JsonConvert.DeserializeObject<TestEnum>(@"""invalid""", converter);
                Assert.AreEqual(TestEnum.Unknown, value);
            }

            [TestMethod]
            public void ShouldParseString()
            {
                EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

                TestEnum value = JsonConvert.DeserializeObject<TestEnum>(@"""Test""", converter);
                Assert.AreEqual(TestEnum.Test, value);
            }

            [TestMethod]
            public void ShouldIgnoreCasingWhenParsingString()
            {
                EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

                TestEnum value = JsonConvert.DeserializeObject<TestEnum>(@"""test""", converter);
                Assert.AreEqual(TestEnum.Test, value);
            }
        }
    }
}
