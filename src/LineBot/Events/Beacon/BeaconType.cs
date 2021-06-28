// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the beacon types.
    /// </summary>
    public enum BeaconType
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown,

        /// <summary>
        /// Tapped beacon banner.
        /// </summary>
        Banner,

        /// <summary>
        /// Entered beacon’s reception range.
        /// </summary>
        Enter,

        /// <summary>
        /// Left beacon’s reception range.
        /// </summary>
        Leave
    }
}
