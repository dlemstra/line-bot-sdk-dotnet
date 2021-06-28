// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    internal sealed class Postback
    {
        [JsonProperty("data")]
        public string Data { get; set; } = default!;
    }
}
