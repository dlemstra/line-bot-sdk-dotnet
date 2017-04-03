// <copyright file="ILineBot.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Threading.Tasks;

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for the bot that can be used to communicatie with the Line API.
    /// </summary>
    public interface ILineBot
    {
        /// <summary>
        /// Returns the profile for the specified user.
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>The profile for the specified user.</returns>
        Task<IUserProfile> GetProfile(string userId);
    }
}
