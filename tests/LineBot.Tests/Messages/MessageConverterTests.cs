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

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or empty.", () =>
            {
                MessageConverter.Convert(new ISendMessage[] { textMessage });
            });
        }

        [TestMethod]
        public void Convert_CustomITextMessage_ConvertedToTextMessage()
        {
            TestTextMessage textMessage = new TestTextMessage();

            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { textMessage });

            Assert.AreEqual(1, messages.Length);
            Assert.AreNotEqual(textMessage, messages[0]);
            Assert.IsInstanceOfType(messages[0], typeof(TextMessage));
        }

        [TestMethod]
        public void Convert_NullValueInArray_ReturnsArray()
        {
            ISendMessage[] messages = MessageConverter.Convert(new ISendMessage[] { null });

            Assert.AreEqual(1, messages.Length);
            Assert.IsNull(messages[0]);
        }

        [TestMethod]
        public void Convert_InvalidType_ReturnsArray()
        {
            ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
            {
                MessageConverter.Convert(new InvalidMessage[1] { new InvalidMessage() });
            });
        }

        [ExcludeFromCodeCoverage]
        private class TestTextMessage : ITextMessage
        {
            public string Text => nameof(TestTextMessage);
        }

        [ExcludeFromCodeCoverage]
        private class InvalidMessage : ISendMessage
        {
        }
    }
}
