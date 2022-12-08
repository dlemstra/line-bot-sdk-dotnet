// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates messages that can be send.
    /// </summary>
    public interface ISendMessage
    {
        internal MessageType Type { get; }

        /// <summary>
        /// Validates the message.
        /// </summary>
        void Validate();
    }
}
