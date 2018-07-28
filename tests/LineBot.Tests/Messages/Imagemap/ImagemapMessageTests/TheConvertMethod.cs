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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests.Messages.Audio
{
    public partial class ImagemapMessageTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsImagemapMessage()
            {
                var message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative",
                    Actions = new ImagemapAction[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                        new ImagemapUriAction("https://url.foo", 1, 2, 3, 4),
                    }
                };

                var imagemapMessage = ImagemapMessage.Convert(message);

                Assert.AreEqual(message, imagemapMessage);
                Assert.AreEqual(message.BaseSize, imagemapMessage.BaseSize);

                var actions = imagemapMessage.Actions.ToArray();
                Assert.AreEqual(message.Actions.First(), actions[0]);

                var action = message.Actions.Skip(1).First();
                Assert.AreEqual(action, actions[1]);
                Assert.AreEqual(action.Area, actions[1].Area);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenBaseUrlIsNull()
            {
                var message = new ImagemapMessage()
                {
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be null.", () =>
                {
                    ImagemapMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenBaseSizeIsNull()
            {
                var message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The base size cannot be null.", () =>
                {
                    ImagemapMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAlternativeTextIsNull()
            {
                var message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null.", () =>
                {
                    ImagemapMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionsIsNull()
            {
                var message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    ImagemapMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIImagemapMessageToImagemapMessage()
            {
                var message = new TestImagemapMessage();

                var imagemapMessage = ImagemapMessage.Convert(message);

                Assert.AreNotEqual(message, imagemapMessage);

                Assert.AreEqual(new Uri("https://foo.url"), imagemapMessage.BaseUrl);
                Assert.AreEqual(1040, imagemapMessage.BaseSize.Width);
                Assert.AreEqual(520, imagemapMessage.BaseSize.Height);
                Assert.AreEqual("Alternative", imagemapMessage.AlternativeText);

                var actions = imagemapMessage.Actions.ToArray();

                var uriAction = actions[0] as ImagemapUriAction;
                Assert.AreEqual(new Uri("https://foo.bar"), uriAction.Url);
                Assert.AreEqual(4, uriAction.Area.X);
                Assert.AreEqual(3, uriAction.Area.Y);
                Assert.AreEqual(2, uriAction.Area.Width);
                Assert.AreEqual(1, uriAction.Area.Height);

                var messageAction = actions[1] as ImagemapMessageAction;
                Assert.AreEqual("TestImagemapMessageAction", messageAction.Text);
                Assert.AreEqual(4, messageAction.Area.X);
                Assert.AreEqual(3, messageAction.Area.Y);
                Assert.AreEqual(2, messageAction.Area.Width);
                Assert.AreEqual(1, messageAction.Area.Height);
            }
        }
    }
}
