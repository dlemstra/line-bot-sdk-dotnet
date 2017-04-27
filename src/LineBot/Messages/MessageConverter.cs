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
using System.Collections.Generic;
using System.Linq;

namespace Line
{
    internal static class MessageConverter
    {
        public static ISendMessage[] Convert(IEnumerable<ISendMessage> messages)
        {
            return Convert(messages.ToArray());
        }

        public static ISendMessage[] Convert(ISendMessage[] messages)
        {
            if (messages.Length > 5)
                throw new InvalidOperationException("The maximum number of messages is 5.");

            ISendMessage[] result = new ISendMessage[messages.Length];

            for (int i = 0; i < messages.Length; i++)
            {
                if (messages[i] == null)
                    throw new InvalidOperationException("The message should not be null.");

                switch (messages[i])
                {
                    case ITextMessage textMessage:
                        result[i] = ToTextMessage(textMessage);
                        break;
                    case IImageMessage imageMessage:
                        result[i] = ToImageMessage(imageMessage);
                        break;
                    case IVideoMessage videoMessage:
                        result[i] = ToVideoMessage(videoMessage);
                        break;
                    default:
                        throw new NotSupportedException("Invalid message type.");
                }
            }

            return result;
        }

        private static ISendMessage ToImageMessage(IImageMessage message)
        {
            ImageMessage imageMessage = message as ImageMessage;

            if (imageMessage == null)
                imageMessage = new ImageMessage(message);
            else
                imageMessage.CheckRequiredFields();

            return imageMessage;
        }

        private static ISendMessage ToTextMessage(ITextMessage message)
        {
            TextMessage textMessage = message as TextMessage;

            if (textMessage == null)
                textMessage = new TextMessage(message);
            else
                textMessage.CheckRequiredFields();

            return textMessage;
        }

        private static ISendMessage ToVideoMessage(IVideoMessage message)
        {
            VideoMessage videoMessage = message as VideoMessage;

            if (videoMessage == null)
                videoMessage = new VideoMessage(message);
            else
                videoMessage.CheckRequiredFields();

            return videoMessage;
        }
    }
}
