// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a location.
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Gets the address of the location.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Gets the latitude of the location.
        /// </summary>
        decimal Latitude { get; }

        /// <summary>
        /// Gets the longitude of the location.
        /// </summary>
        decimal Longitude { get; }

        /// <summary>
        /// Gets the title of the location.
        /// </summary>
        string Title { get; }
    }
}
