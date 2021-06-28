// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class LineError
    {
        [JsonProperty("message")]
        public string Message { get; set; } = default!;

        [JsonProperty("details")]
        public IEnumerable<LineErrorDetails> Details { get; set; } = Enumerable.Empty<LineErrorDetails>();
    }
}
