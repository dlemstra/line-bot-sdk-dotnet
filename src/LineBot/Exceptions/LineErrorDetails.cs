// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    internal sealed class LineErrorDetails : ILineErrorDetails
    {
        [JsonProperty("message")]
        public string Message { get; set; } = default!;

        [JsonProperty("property")]
        public string Property { get; set; } = default!;
    }
}
