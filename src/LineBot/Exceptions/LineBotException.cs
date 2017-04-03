// <copyright file="LineBotException.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net;

namespace Line
{
    /// <summary>
    /// Reports errors that occur when communicating with the Line API.
    /// </summary>
    public sealed class LineBotException : Exception
    {
        internal LineBotException(HttpStatusCode statusCode)
            : base("Unknown error")
        {
            StatusCode = statusCode;
            Details = new List<ILineErrorDetails>();
        }

        internal LineBotException(HttpStatusCode statusCode, LineError error)
            : base(error.Message)
        {
            StatusCode = statusCode;
            Details = error.Details;
        }

        /// <summary>
        /// Gets the status code that was reported by the Line API.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the details of the error that occurred.
        /// </summary>
        public IEnumerable<ILineErrorDetails> Details { get; }
    }
}
