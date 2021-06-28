// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    internal sealed class Beacon
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<BeaconType>))]
        public BeaconType BeaconType { get; set; }

        [JsonProperty("hwid")]
        public string? Hwid { get; set; }
    }
}
