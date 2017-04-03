// <copyright file="LineConfiguration.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace Line
{
    /// <summary>
    /// Encapsulates the configuration of the line bot.
    /// </summary>
    public sealed class LineConfiguration : ILineConfiguration
    {
        /// <summary>
        /// Gets or sets the channel access token.
        /// </summary>
        public string ChannelAccessToken { get; set; }
    }
}
