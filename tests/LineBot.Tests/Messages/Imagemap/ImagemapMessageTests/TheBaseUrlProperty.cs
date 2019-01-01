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

namespace Line.Tests
{
    public partial class ImagemapMessageTests
    {
        [TestClass]
        public class TheBaseUrlProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be null.", () =>
                {
                    message.BaseUrl = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The base url should use the https scheme.", () =>
                {
                    message.BaseUrl = new Uri("http://foo.bar");
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be longer than 1000 characters.", () =>
                {
                    message.BaseUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenUrlIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var message = new ImagemapMessage()
                {
                    BaseUrl = value
                };

                Assert.AreEqual(value, message.BaseUrl);
            }
        }
    }
}
