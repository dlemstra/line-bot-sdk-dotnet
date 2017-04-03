// <copyright file="LineErrorDetails.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using Newtonsoft.Json;

namespace Line
{
    internal sealed class LineErrorDetails : ILineErrorDetails
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("property")]
        public string Property { get; set; }
    }
}
