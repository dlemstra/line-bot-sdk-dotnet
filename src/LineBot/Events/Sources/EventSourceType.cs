// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// /// Encapsulates the type of the source.
    /// </summary>
    public enum EventSourceType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unkown,

        /// <summary>
        /// Group.
        /// </summary>
        Group,

        /// <summary>
        /// Room.
        /// </summary>
        Room,

        /// <summary>
        /// User.
        /// </summary>
        User,
    }
}
