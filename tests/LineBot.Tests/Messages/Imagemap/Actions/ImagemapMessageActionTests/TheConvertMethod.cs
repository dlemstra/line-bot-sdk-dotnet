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
    public partial class ImagemapMessageActionTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsImagemapMessageAction()
            {
                var action = new ImagemapMessageAction("Text", 1, 2, 3, 4);

                var messageAction = ImagemapMessageAction.Convert(action);

                Assert.AreEqual(action, messageAction);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                var action = new ImagemapMessageAction()
                {
                    Area = new ImagemapArea(1, 2, 3, 4)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    ImagemapMessageAction.Convert(action);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAreaIsNull()
            {
                var action = new ImagemapMessageAction()
                {
                    Text = "test"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    ImagemapMessageAction.Convert(action);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIImagemapMessageActionToImagemapMessageAction()
            {
                var action = new TestImagemapMessageAction();

                var messageAction = ImagemapMessageAction.Convert(action);

                Assert.AreNotEqual(action, messageAction);
                Assert.AreEqual("TestImagemapMessageAction", messageAction.Text);
                Assert.IsNotNull(messageAction.Area);
                Assert.AreEqual(4, messageAction.Area.X);
                Assert.AreEqual(3, messageAction.Area.Y);
                Assert.AreEqual(2, messageAction.Area.Width);
                Assert.AreEqual(1, messageAction.Area.Height);
            }
        }
    }
}
