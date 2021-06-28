// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a sticker.
    /// </summary>
    public interface ISticker
    {
        /// <summary>
        /// Gets the id of the package.
        /// </summary>
        string PackageId { get; }

        /// <summary>
        /// Gets the id of the sticker.
        /// </summary>
        string StickerId { get; }
    }
}
