// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;

namespace Line
{
    /// <summary>
    /// Encapsulates the configuration of the <see cref="ILineBot"/>.
    /// </summary>
    public sealed class LineConfiguration : ILineConfiguration
    {
        /// <summary>
        /// Gets or sets the channel access token.
        /// </summary>
        public string ChannelAccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the channel secret.
        /// </summary>
        public string ChannelSecret { get; set; } = string.Empty;
    }
}
