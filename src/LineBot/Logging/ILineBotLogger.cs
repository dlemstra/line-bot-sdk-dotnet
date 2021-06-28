// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Line
{
    /// <summary>
    /// Encapsulates a logger that can be used to log the requests and responses.
    /// </summary>
    public interface ILineBotLogger
    {
        /// <summary>
        /// Logs the uri and the data that was send.
        /// </summary>
        /// <param name="uri">The uri that was requested.</param>
        /// <param name="httpContent">The content of the request that was send.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task LogApiCall(Uri uri, HttpContent httpContent);

        /// <summary>
        /// Logs the data of the events that were received.
        /// </summary>
        /// <param name="eventsData">The data of the events that were received.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task LogReceivedEvents(byte[] eventsData);
    }
}
