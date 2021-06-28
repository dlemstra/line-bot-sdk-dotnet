// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;

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
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="SourceType"/> is not <see cref="EventSourceType.Group"/>.</exception>
        /// <returns>The group.</returns>
        IGroup? Group { get; }

        /// <summary>
        /// Gets the room.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="SourceType"/> is not <see cref="EventSourceType.Room"/>.</exception>
        /// <returns>The room.</returns>
        IRoom? Room { get; }

        /// <summary>
        /// Gets the type of the source.
        /// </summary>
        EventSourceType SourceType { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="SourceType"/> is not <see cref="EventSourceType.User"/>.</exception>
        /// <returns>The user.</returns>
        IUser? User { get; }
    }
}
