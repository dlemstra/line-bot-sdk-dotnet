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
                        result[i] = TextMessage.Convert(textMessage);
                        break;
                    case IImageMessage imageMessage:
                        result[i] = ImageMessage.Convert(imageMessage);
                        break;
                    case IVideoMessage videoMessage:
                        result[i] = VideoMessage.Convert(videoMessage);
                        break;
                    case IAudioMessage audioMessage:
                        result[i] = AudioMessage.Convert(audioMessage);
                        break;
                    case ILocationMessage locationMessage:
                        result[i] = LocationMessage.Convert(locationMessage);
                        break;
                    case IStickerMessage stickerMessage:
                        result[i] = stickerMessage.ToStickerMessage();
                        break;
                    case IImagemapMessage imagemapMessage:
                        result[i] = imagemapMessage.ToImagemapMessage();
                        break;
                    case ITemplateMessage templateMessage:
                        result[i] = templateMessage.ToTemplateMessage();
                        break;
                    default:
                        throw new NotSupportedException("Invalid message type.");
                }
            }

            return result;
        }
    }
}
