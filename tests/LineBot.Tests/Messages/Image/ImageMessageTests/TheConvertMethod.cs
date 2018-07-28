// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsImageMessage()
            {
                var message = new ImageMessage()
                {
                    Url = new Uri("https://foo.url"),
                    PreviewUrl = new Uri("https://foo.previewUrl")
                };

                var imageMessage = ImageMessage.Convert(message);

                Assert.AreEqual(message, imageMessage);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenUrlIsNull()
            {
                var message = new ImageMessage()
                {
                    PreviewUrl = new Uri("https://foo.previewUrl")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    ImageMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPreviewUrlIsNull()
            {
                var message = new ImageMessage()
                {
                    Url = new Uri("https://foo.url")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The preview url cannot be null.", () =>
                {
                    ImageMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIImageMessageToImageMessage()
            {
                var message = new TestImageMessage();

                var imageMessage = ImageMessage.Convert(message);

                Assert.AreNotEqual(message, imageMessage);
                Assert.AreEqual(new Uri("https://foo.url"), imageMessage.Url);
                Assert.AreEqual(new Uri("https://foo.previewUrl"), imageMessage.PreviewUrl);
            }
        }
    }
}
