// <copyright file="LineError.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class LineError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("details")]
        public IEnumerable<LineErrorDetails> Details { get; set; }
    }
}
