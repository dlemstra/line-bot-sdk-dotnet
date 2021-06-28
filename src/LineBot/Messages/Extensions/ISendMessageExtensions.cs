// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
    internal static class ISendMessageExtensions
    {
        public static ISendMessage[] ValidateAndConvert(this IEnumerable<ISendMessage> messages)
        {
            var result = messages.ToArray();

            Validate(result);

            return result;
        }

        public static void Validate(this ISendMessage[] messages)
        {
            if (messages.Length > 5)
                throw new InvalidOperationException("The maximum number of messages is 5.");

            foreach (var message in messages)
            {
                if (message is null)
                    throw new InvalidOperationException("The message should not be null.");

                if (IsInvalidMessageType(message))
                    throw new NotSupportedException("Invalid message type.");

                message.Validate();
            }
        }

        private static bool IsInvalidMessageType(ISendMessage? message)
        {
            if (message is null)
                return false;

            if (message.Type == MessageType.Text)
                return false;

            if (message.Type == MessageType.Image)
                return false;

            if (message.Type == MessageType.Video)
                return false;

            if (message.Type == MessageType.Audio)
                return false;

            if (message.Type == MessageType.Location)
                return false;

            if (message.Type == MessageType.Sticker)
                return false;

            if (message.Type == MessageType.Imagemap)
                return false;

            if (message.Type == MessageType.Template)
                return false;

            return true;
        }
    }
}
