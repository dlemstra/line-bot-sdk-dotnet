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
    public partial class ISendMessageExtensionsTests
    {
        [TestClass]
        public class TheValidateMethod : ISendMessageExtensionsTests
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCalledWithMoreThanFiveMessages()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of messages is 5.", () =>
                {
                    ISendMessageExtensions.Validate(new ISendMessage[6]);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrarHasNullValue()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The message should not be null.", () =>
                {
                    ISendMessageExtensions.Validate(new ISendMessage[1] { null });
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMessageTypeIsInvalid()
            {
                ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
                {
                    ISendMessageExtensions.Validate(new ISendMessage[1] { new InvalidMessage() });
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMessageIsInvalid()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    ISendMessageExtensions.Validate(new ISendMessage[1] { new TextMessage() });
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMessageIsTextMessage()
            {
                var message = new TextMessage() { Text = "Foo" };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMessageIsImageMessage()
            {
                var message = new ImageMessage()
                {
                    Url = new Uri("https://foo.bar"),
                    PreviewUrl = new Uri("https://bar.foo")
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMessageIsVideoMessage()
            {
                var message = new VideoMessage()
                {
                    Url = new Uri("https://foo.bar"),
                    PreviewUrl = new Uri("https://bar.foo")
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMessageIsAudioMessage()
            {
                var message = new AudioMessage()
                {
                    Url = new Uri("https://foo.bar"),
                    Duration = 1
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMessageIsLocationMessage()
            {
                var message = new LocationMessage()
                {
                    Title = "Foo",
                    Address = "Bar"
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMessageIsStickerMessage()
            {
                var message = new StickerMessage()
                {
                    PackageId = "1",
                    StickerId = "2"
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMessageIsImagemapMessage()
            {
                var message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    AlternativeText = "Foo",
                    BaseSize = new ImagemapSize(10, 10),
                    Actions = new[] { new ImagemapUriAction("https://foo.bar", new ImagemapArea(1, 2, 3, 4)) }
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenMessageIsTemplageMessage()
            {
                var message = new TemplateMessage()
                {
                    AlternativeText = "Foo",
                    Template = new ButtonsTemplate()
                    {
                        Text = "Foo",
                        Actions = new[] { new MessageAction() { Label = "Foo", Text = "Bar" } }
                    }
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }
        }
    }
}
