// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a LINE event.
    /// </summary>
    public interface ILineEvent : IReplyToken
    {
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        LineEventType EventType { get; }

        /// <summary>
        /// Gets the beacon information.
        /// </summary>
        IBeacon? Beacon { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        IMessage? Message { get; }

        /// <summary>
        /// Gets the postback information.
        /// </summary>
        IPostback? Postback { get; }

        /// <summary>
        /// Gets the source of the event.
        /// </summary>
        IEventSource? Source { get; }

        /// <summary>
        /// Gets the time of the event.
        /// </summary>
        DateTime Timestamp { get; }
    }
}
