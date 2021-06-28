// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for the configuration of the <see cref="ILineBot"/>.
    /// </summary>
    public interface ILineConfiguration
    {
        /// <summary>
        /// Gets the channel access token.
        /// </summary>
        string ChannelAccessToken { get; }

        /// <summary>
        /// Gets the channel secret.
        /// </summary>
        string ChannelSecret { get; }
    }
}