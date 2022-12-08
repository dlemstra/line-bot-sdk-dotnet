// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    internal sealed class RichMenuResponse : RichMenu, IRichMenuResponse
    {
        [JsonProperty("richMenuId")]
        public string Id { get; set; } = default!;
    }
}
