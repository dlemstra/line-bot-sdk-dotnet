// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class MulticastMessage
    {
        public MulticastMessage(IEnumerable<string> to, IEnumerable<ISendMessage> messages)
        {
            To = to;
            Messages = messages.ValidateAndConvert();
        }

        [JsonProperty("to")]
        public IEnumerable<string> To { get; }

        [JsonProperty("messages")]
        public ISendMessage[] Messages { get; }
    }
}
