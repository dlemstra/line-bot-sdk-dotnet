// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the room.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Gets the id of the room.
        /// </summary>
        string Id { get; }
    }
}
