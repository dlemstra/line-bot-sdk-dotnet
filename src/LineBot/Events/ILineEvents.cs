// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a list of line events.
    /// </summary>
    public interface ILineEvents : IEnumerable<ILineEvent>
    {
        /// <summary>
        /// Gets the user ID of a bot that should receive webhook events.
        /// </summary>
        string Destination { get; }

        /// <summary>
        /// Gets all the events.
        /// </summary>
        IEnumerable<ILineEvent> Events { get; }
    }
}
