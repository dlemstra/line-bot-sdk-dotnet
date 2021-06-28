// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a message.
    /// </summary>
    public interface IMessage : IReplyToken
    {
        /// <summary>
        /// Gets the id of the messge.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the location data of the message.
        /// </summary>
        ILocation Location { get; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        MessageType MessageType { get; }

        /// <summary>
        /// Gets the sticker data of the message.
        /// </summary>
        ISticker Sticker { get; }

        /// <summary>
        /// Gets the text of the message.
        /// </summary>
        string Text { get; }
    }
}
