// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a beacon.
    /// </summary>
    public interface IBeacon : IReplyToken
    {
        /// <summary>
        /// Gets the type of the beacon.
        /// </summary>
        BeaconType BeaconType { get; }

        /// <summary>
        /// Gets the hardware ID of the beacon that was detected.
        /// </summary>
        string Hwid { get; }
    }
}
