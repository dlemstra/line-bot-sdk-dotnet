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

namespace Line.Tests
{
    public partial class MessageConverterTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCalledWithMoreThanFiveMessages()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of messages is 5.", () =>
                {
                    MessageConverter.Convert(new IOldSendMessage[6]);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrarHasNullValue()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The message should not be null.", () =>
                {
                    MessageConverter.Convert(new IOldSendMessage[1] { null });
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMessageTypeIsInvalid()
            {
                ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
                {
                    MessageConverter.Convert(new InvalidMessage[1] { new InvalidMessage() });
                });
            }

            [TestMethod]
            public void ShouldConvertCustomITextMessageToTextMessage()
            {
                var message = new TestTextMessage();

                var messages = MessageConverter.Convert(new IOldSendMessage[] { message });

                Assert.AreEqual(1, messages.Length);
                Assert.AreNotEqual(message, messages[0]);

                var textMessage = messages[0] as TextMessage;
                Assert.AreEqual("TestTextMessage", textMessage.Text);
            }

            [TestMethod]
            public void ShouldConvertCustomIVideoMessageToImageMessage()
            {
                var message = new TestVideoMessage();

                var messages = MessageConverter.Convert(new IOldSendMessage[] { message });

                Assert.AreEqual(1, messages.Length);
                Assert.AreNotEqual(message, messages[0]);

                var videoMessage = messages[0] as VideoMessage;
                Assert.AreEqual(new Uri("https://foo.url"), videoMessage.Url);
                Assert.AreEqual(new Uri("https://foo.previewUrl"), videoMessage.PreviewUrl);
            }

            [TestMethod]
            public void ShouldConvertCustomIImagemapMessageToImagemapMessage()
            {
                var message = new TestImagemapMessage();

                var messages = MessageConverter.Convert(new IOldSendMessage[] { message });

                Assert.AreEqual(1, messages.Length);
                Assert.AreNotEqual(message, messages[0]);

                var imagemapMessage = messages[0] as ImagemapMessage;
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

            [TestMethod]
            public void ShouldConvertCustomITemplateMessageToTemplateMessage()
            {
                var message = new TestTemplateMessage()
                {
                    Template = new TestButtonsTemplate()
                };

                var messages = MessageConverter.Convert(new IOldSendMessage[] { message });

                Assert.AreEqual(1, messages.Length);
                Assert.AreNotEqual(message, messages[0]);

                var templateMessage = messages[0] as TemplateMessage;
                Assert.AreEqual("AlternativeText", templateMessage.AlternativeText);

                var template = templateMessage.Template as ButtonsTemplate;
                Assert.AreEqual(new Uri("https://bar.foo"), template.ThumbnailUrl);
                Assert.AreEqual("ButtonsTitle", template.Title);
                Assert.AreEqual("ButtonsText", template.Text);
            }

            [TestMethod]
            public void ShouldConvertCustomICarouselTemplateToCarouselTemplate()
            {
                var message = new TestTemplateMessage()
                {
                    Template = new TestCarouselTemplate()
                };

                var messages = MessageConverter.Convert(new IOldSendMessage[] { message });

                Assert.AreEqual(1, messages.Length);
                Assert.AreNotEqual(message, messages[0]);

                var templateMessage = messages[0] as TemplateMessage;
                Assert.AreEqual("AlternativeText", templateMessage.AlternativeText);

                var template = templateMessage.Template as CarouselTemplate;

                var column = template.Columns.First() as CarouselColumn;
                Assert.AreEqual(new Uri("https://carousel.url"), column.ThumbnailUrl);
                Assert.AreEqual("CarouselTitle", column.Title);
                Assert.AreEqual("CarouselText", column.Text);
            }

            [TestMethod]
            public void ShouldConvertCustomIConfirmTemplateToConfirmTemplate()
            {
                var message = new TestTemplateMessage()
                {
                    Template = new TestConfirmTemplate()
                };

                var messages = MessageConverter.Convert(new IOldSendMessage[] { message });

                Assert.AreEqual(1, messages.Length);
                Assert.AreNotEqual(message, messages[0]);

                var templateMessage = messages[0] as TemplateMessage;
                Assert.AreEqual("AlternativeText", templateMessage.AlternativeText);

                var template = templateMessage.Template as ConfirmTemplate;
                Assert.AreEqual("ConfirmText", template.Text);
            }

            [ExcludeFromCodeCoverage]
            private class InvalidMessage : IOldSendMessage
            {
            }
        }
    }
}
