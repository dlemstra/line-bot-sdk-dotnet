// <copyright file="IUserProfile.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for the profile of a user.
    /// </summary>
    public interface IUserProfile
    {
        /// <summary>
        /// Gets the display name of the user.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the url of the picture of the user.
        /// </summary>
        Uri PictureUrl { get; }

        /// <summary>
        /// Gets the status message of the user.
        /// </summary>
        string StatusMessage { get; }

        /// <summary>
        /// Gets the id of the user.
        /// </summary>
        string UserId { get; }
    }
}