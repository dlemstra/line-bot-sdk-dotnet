// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates the details of an error.
    /// </summary>
    public interface ILineErrorDetails
    {
        /// <summary>
        /// Gets the message of the error.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets the property that caused the error.
        /// </summary>
        [JsonProperty("property")]
        string Property { get; }
    }
}
