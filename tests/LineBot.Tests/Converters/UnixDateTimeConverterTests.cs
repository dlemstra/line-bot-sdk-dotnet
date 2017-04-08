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

namespace Line.Tests
{
    [TestClass]
    public class UnixDateTimeConverterTests
    {
        [TestMethod]
        public void CanConvert_DateTime_ReturnsTrue()
        {
            UnixDateTimeConverter converter = new UnixDateTimeConverter();

            Assert.IsTrue(converter.CanConvert(typeof(DateTime)));
        }

        [TestMethod]
        public void CanConvert_NullableDateTime_ReturnsTrue()
        {
            UnixDateTimeConverter converter = new UnixDateTimeConverter();

            Assert.IsFalse(converter.CanConvert(typeof(DateTime?)));
        }

        [TestMethod]
        public void CanConvert_CanRead_ReturnsTrue()
        {
            UnixDateTimeConverter converter = new UnixDateTimeConverter();

            Assert.IsTrue(converter.CanRead);
        }

        [TestMethod]
        public void CanConvert_CanWrite_ReturnsFalse()
        {
            UnixDateTimeConverter converter = new UnixDateTimeConverter();

            Assert.IsFalse(converter.CanWrite);
        }

        [TestMethod]
        public void WriteJson_ThrowsException()
        {
            UnixDateTimeConverter converter = new UnixDateTimeConverter();

            ExceptionAssert.Throws<NotSupportedException>(() =>
            {
                converter.WriteJson(null, null, null);
            });
        }

        [TestMethod]
        public void ReadJson_StringValue_ThrowsException()
        {
            UnixDateTimeConverter converter = new UnixDateTimeConverter();

            ExceptionAssert.Throws<InvalidOperationException>("Only integer is supported.", () =>
            {
                JsonConvert.DeserializeObject<DateTime>(@"""1234567890""", converter);
            });
        }

        [TestMethod]
        public void ReadJson_ValueIsZero_ReturnsEpochUtc()
        {
            UnixDateTimeConverter converter = new UnixDateTimeConverter();

            DateTime value = JsonConvert.DeserializeObject<DateTime>("0", converter);

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
            Assert.AreEqual(epoch, value);
            Assert.AreEqual(DateTimeKind.Utc, value.Kind);
        }

        [TestMethod]
        public void ReadJson_ValueIs2000_ReturnsEpochUtcPlusTwoSeconds()
        {
            UnixDateTimeConverter converter = new UnixDateTimeConverter();

            DateTime value = JsonConvert.DeserializeObject<DateTime>("2000", converter);

            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 2);
            Assert.AreEqual(epoch, value);
            Assert.AreEqual(DateTimeKind.Utc, value.Kind);
        }
    }
}