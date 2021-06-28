// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the line event types.
    /// </summary>
    public enum LineEventType
    {
        /// <summary>
        /// Unknown event type.
        /// </summary>
        Unknown,

        /// <summary>
        /// Beacon event type.
        /// </summary>
        Beacon,

        /// <summary>
        /// Follow event type.
        /// </summary>
        Follow,

        /// <summary>
        /// Join event type.
        /// </summary>
        Join,

        /// <summary>
        /// Leave event type.
        /// </summary>
        Leave,

        /// <summary>
        /// Message event type.
        /// </summary>
        Message,

        /// <summary>
        /// Postback event type.
        /// </summary>
        Postback,

        /// <summary>
        /// Unfollow event type.
        /// </summary>
        Unfollow
    }
}
