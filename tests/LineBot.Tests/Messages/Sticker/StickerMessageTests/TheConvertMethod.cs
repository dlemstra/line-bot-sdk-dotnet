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
    public partial class StickerMessageTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsStickerMessage()
            {
                var message = new StickerMessage()
                {
                    PackageId = "PackageId",
                    StickerId = "StickerId"
                };

                var stickerMessage = StickerMessage.Convert(message);

                Assert.AreEqual(message, stickerMessage);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenPackageIdIsNull()
            {
                var message = new StickerMessage()
                {
                    StickerId = "StickerId"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The package id cannot be null.", () =>
                {
                    StickerMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStickerIdIsNull()
            {
                var message = new StickerMessage()
                {
                    PackageId = "PackageId"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The sticker id cannot be null.", () =>
                {
                    StickerMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIStickerMessageToStickerMessage()
            {
                var message = new TestStickerMessage();

                var stickerMessage = StickerMessage.Convert(message);

                Assert.AreNotEqual(message, stickerMessage);

                Assert.AreEqual("PackageId", stickerMessage.PackageId);
                Assert.AreEqual("StickerId", stickerMessage.StickerId);
            }
        }
    }
}
