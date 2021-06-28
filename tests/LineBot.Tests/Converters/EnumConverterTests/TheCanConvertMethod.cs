// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        [TestClass]
        public class TheCanConvertMethod
        {
            [TestMethod]
            public void ShouldReturnTrueWhenTypeIsEnum()
            {
                EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

                Assert.IsTrue(converter.CanConvert(typeof(TestEnum)));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenTypeIsNullableEnum()
            {
                EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

                Assert.IsFalse(converter.CanConvert(typeof(TestEnum?)));
            }
        }
    }
}