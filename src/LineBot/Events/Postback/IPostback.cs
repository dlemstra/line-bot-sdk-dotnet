// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a postback.
    /// </summary>
    public interface IPostback : IReplyToken
    {
        /// <summary>
        /// Gets the postback data.
        /// </summary>
        string Data { get; }
    }
}
