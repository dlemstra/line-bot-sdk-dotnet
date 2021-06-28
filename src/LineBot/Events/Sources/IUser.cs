// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the user.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets the id of the user.
        /// </summary>
        string Id { get; }
    }
}
