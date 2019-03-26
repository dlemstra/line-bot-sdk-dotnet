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
    public partial class ImageMessageTests
    {
        [TestClass]
        public class ThePreviewUrlProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new ImageMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    message.PreviewUrl = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                var message = new ImageMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The url should use the https scheme.", () =>
                {
                    message.PreviewUrl = new Uri("http://foo.bar");
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var message = new ImageMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 1000 characters.", () =>
                {
                    message.PreviewUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenUrlIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var message = new ImageMessage()
                {
                    PreviewUrl = value
                };

                Assert.AreEqual(value, message.PreviewUrl);
            }
        }
    }
}
