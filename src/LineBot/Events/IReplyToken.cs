// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a reply token.
    /// </summary>
    public interface IReplyToken
    {
        /// <summary>
        /// Gets the reply token.
        /// </summary>
        string? ReplyToken { get; }
    }
}
