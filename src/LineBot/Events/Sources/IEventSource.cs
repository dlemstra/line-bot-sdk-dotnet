// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the event source.
    /// </summary>
    public interface IEventSource
    {
        /// <summary>
        /// Gets the group.
        /// </summary>
        /// <returns>The group.</returns>
        IGroup? Group { get; }

        /// <summary>
        /// Gets the room.
        /// </summary>
        /// <returns>The room.</returns>
        IRoom? Room { get; }

        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        EventSourceType SourceType { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>The user.</returns>
        IUser? User { get; }
    }
}
