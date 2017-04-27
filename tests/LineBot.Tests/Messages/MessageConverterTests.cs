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

namespace Line.Tests.Messages
{
    [TestClass]
    public class MessageConverterTests
    {
        [TestMethod]
        public void Convert_TooManyMessagesAreNull_ThrowsException()
        {
            ExceptionAssert.Throws<InvalidOperationException>("The maximum number of messages is 5.", () =>
            {
                MessageConverter.Convert(new ISendMessage[6]);
            });
        }

        [TestMethod]
        public void Convert_NullValueInArray_ThrowsException()
        {
            ExceptionAssert.Throws<InvalidOperationException>("The message should not be null.", () =>
            {
                MessageConverter.Convert(new InvalidMessage[1] { null });
            });
        }

        [TestMethod]
        public void Convert_InvalidType_ThrowsException()
        {
            ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
            {
                MessageConverter.Convert(new InvalidMessage[1] { new InvalidMessage() });
            });
        }

        [TestMethod]
        public void Convert_TextMessage_InstanceIsPreserved()
        {
            TextMessage textMessage = new TextMessage()
            {
                Text = "Test"
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { textMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(textMessage, messages[0]);
        }

        [TestMethod]
        public void Convert_TextMessageWithoutText_ThrowsException()
        {
            TextMessage textMessage = new TextMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { textMessage });
            });
        }

        [TestMethod]
        public void Convert_CustomITextMessage_ConvertedToTextMessage()
        {
            TestTextMessage customTextMessage = new TestTextMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { customTextMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(customTextMessage, messages[0]);

            TextMessage textMessage = messages[0] as TextMessage;
            Assert.AreEqual("TestTextMessage", textMessage.Text);
        }

        [TestMethod]
        public void Convert_ImageMessage_InstanceIsPreserved()
        {
            ImageMessage imageMessage = new ImageMessage()
            {
                PreviewUrl = new Uri("https://foo.previewUrl"),
                Url = new Uri("https://foo.url")
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { imageMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(imageMessage, messages[0]);
        }

        [TestMethod]
        public void Convert_ImageMessageWithoutUrl_ThrowsException()
        {
            ImageMessage imageMessage = new ImageMessage()
            {
                PreviewUrl = new Uri("https://foo.previewUrl")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { imageMessage });
            });
        }

        [TestMethod]
        public void Convert_ImageMessageWithoutPreviewUrl_ThrowsException()
        {
            ImageMessage imageMessage = new ImageMessage()
            {
                Url = new Uri("https://foo.url")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The preview url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { imageMessage });
            });
        }

        [TestMethod]
        public void Convert_CustomIImageMessage_ConvertedToImageMessage()
        {
            TestImageMessage customImageMessage = new TestImageMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { customImageMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(customImageMessage, messages[0]);

            ImageMessage imageMessage = messages[0] as ImageMessage;
            Assert.AreEqual(new Uri("https://foo.url"), imageMessage.Url);
            Assert.AreEqual(new Uri("https://foo.previewUrl"), imageMessage.PreviewUrl);
        }

        [TestMethod]
        public void Convert_VideoMessage_InstanceIsPreserved()
        {
            VideoMessage videoMessage = new VideoMessage()
            {
                PreviewUrl = new Uri("https://foo.previewUrl"),
                Url = new Uri("https://foo.url")
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { videoMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(videoMessage, messages[0]);
        }

        [TestMethod]
        public void Convert_VideoMessageWithoutUrl_ThrowsException()
        {
            VideoMessage videoMessage = new VideoMessage()
            {
                PreviewUrl = new Uri("https://foo.previewUrl")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { videoMessage });
            });
        }

        [TestMethod]
        public void Convert_VideoMessageWithoutPreviewUrl_ThrowsException()
        {
            VideoMessage videoMessage = new VideoMessage()
            {
                Url = new Uri("https://foo.url")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The preview url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { videoMessage });
            });
        }

        [TestMethod]
        public void Convert_CustomIVideoMessage_ConvertedToVideoMessage()
        {
            TestVideoMessage customVideoMessage = new TestVideoMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { customVideoMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(customVideoMessage, messages[0]);

            VideoMessage videoMessage = messages[0] as VideoMessage;
            Assert.AreEqual(new Uri("https://foo.url"), videoMessage.Url);
            Assert.AreEqual(new Uri("https://foo.previewUrl"), videoMessage.PreviewUrl);
        }

        [TestMethod]
        public void Convert_AudioMessage_InstanceIsPreserved()
        {
            AudioMessage audioMessage = new AudioMessage()
            {
                Url = new Uri("https://foo.url"),
                Duration = 10000
            };

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { audioMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreEqual(audioMessage, messages[0]);
        }

        [TestMethod]
        public void Convert_AudioMessageWithoutUrl_ThrowsException()
        {
            AudioMessage audioMessage = new AudioMessage()
            {
                Duration = 10000
            };

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { audioMessage });
            });
        }

        [TestMethod]
        public void Convert_VideoMessageWithoutDuration_ThrowsException()
        {
            AudioMessage audioMessage = new AudioMessage()
            {
                Url = new Uri("https://foo.url")
            };

            ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { audioMessage });
            });
        }

        [TestMethod]
        public void Convert_CustomIAudioMessage_ConvertedToAudioMessage()
        {
            TestAudioMessage customAudioMessage = new TestAudioMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { customAudioMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(customAudioMessage, messages[0]);

            AudioMessage audioMessage = messages[0] as AudioMessage;
            Assert.AreEqual(new Uri("https://foo.url"), audioMessage.Url);
            Assert.AreEqual(1000, audioMessage.Duration);
        }

        [ExcludeFromCodeCoverage]
        private class TestTextMessage : ITextMessage
        {
            public string Text => nameof(TestTextMessage);
        }

        [ExcludeFromCodeCoverage]
        private class TestImageMessage : IImageMessage
        {
            public Uri Url => new Uri("https://foo.url");

            public Uri PreviewUrl => new Uri("https://foo.previewUrl");
        }

        [ExcludeFromCodeCoverage]
        private class TestVideoMessage : IVideoMessage
        {
            public Uri Url => new Uri("https://foo.url");

            public Uri PreviewUrl => new Uri("https://foo.previewUrl");
        }

        [ExcludeFromCodeCoverage]
        private class TestAudioMessage : IAudioMessage
        {
            public Uri Url => new Uri("https://foo.url");

            public int Duration => 1000;
        }

        [ExcludeFromCodeCoverage]
        private class InvalidMessage : ISendMessage
        {
        }
    }
}
