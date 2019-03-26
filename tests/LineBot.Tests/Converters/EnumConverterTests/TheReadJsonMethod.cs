// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
