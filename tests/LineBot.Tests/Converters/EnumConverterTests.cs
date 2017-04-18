// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests.Converters
{
    [TestClass]
    public class EnumConverterTests
    {
        private enum TestEnum
        {
            Unknown,
            Test
        }

        [TestMethod]
        public void CanConvert_TestEnum_ReturnsTrue()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            Assert.IsTrue(converter.CanConvert(typeof(TestEnum)));
        }

        [TestMethod]
        public void CanConvert_NullableTestEnum_ReturnsTrue()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            Assert.IsFalse(converter.CanConvert(typeof(TestEnum?)));
        }

        [TestMethod]
        public void CanConvert_CanRead_ReturnsTrue()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            Assert.IsTrue(converter.CanRead);
        }

        [TestMethod]
        public void CanConvert_CanWrite_ReturnsTrue()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            Assert.IsTrue(converter.CanWrite);
        }

        [TestMethod]
        public void ReadJson_IntegerValue_ThrowsException()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            ExceptionAssert.Throws<InvalidOperationException>("Only string is supported.", () =>
            {
                JsonConvert.DeserializeObject<TestEnum>("42", converter);
            });
        }

        [TestMethod]
        public void ReadJson_InvalidValue_ReturnsDefault()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            TestEnum value = JsonConvert.DeserializeObject<TestEnum>(@"""invalid""", converter);
            Assert.AreEqual(TestEnum.Unknown, value);
        }

        [TestMethod]
        public void ReadJson_CorrectValueWithIncorrectCasing_ReturnsEnumValue()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            TestEnum value = JsonConvert.DeserializeObject<TestEnum>(@"""test""", converter);
            Assert.AreEqual(TestEnum.Test, value);
        }

        [TestMethod]
        public void ReadJson_CorrectValueWithCorrectCasing_ReturnsEnumValue()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            TestEnum value = JsonConvert.DeserializeObject<TestEnum>(@"""Test""", converter);
            Assert.AreEqual(TestEnum.Test, value);
        }

        [TestMethod]
        public void WriteJson_CorrectValue_ReturnsLowercaseString()
        {
            EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

            string value = JsonConvert.SerializeObject(TestEnum.Test, converter);
            Assert.AreEqual(@"""test""", value);
        }
    }
}