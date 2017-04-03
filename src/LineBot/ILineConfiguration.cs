// <copyright file="ILineConfiguration.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for the configuration of the line bot.
    /// </summary>
    public interface ILineConfiguration
    {
        /// <summary>
        /// Gets the channel access token.
        /// </summary>
        string ChannelAccessToken { get; }
    }
}