// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the message types.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Audio message.
        /// </summary>
        Audio,

        /// <summary>
        /// Image message.
        /// </summary>
        Image,

        /// <summary>
        /// Imagemap message.
        /// </summary>
        Imagemap,

        /// <summary>
        /// Location message.
        /// </summary>
        Location,

        /// <summary>
        /// Sticker message.
        /// </summary>
        Sticker,

        /// <summary>
        /// Template message.
        /// </summary>
        Template,

        /// <summary>
        /// Text message.
        /// </summary>
        Text,

        /// <summary>
        /// Video message.
        /// </summary>
        Video
    }
}
