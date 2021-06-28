// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
