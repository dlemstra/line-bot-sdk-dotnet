// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Line
{
    [JsonObject]
    internal sealed class LineEventCollection : ILineEvents
    {
        [JsonProperty("destination")]
        public string Destination { get; set; } = default!;

        [JsonProperty("events")]
        public List<LineEvent> Events { get; set; } = default!;

        IEnumerable<ILineEvent> ILineEvents.Events => Events;

        IEnumerator IEnumerable.GetEnumerator() => Events.GetEnumerator();

        IEnumerator<ILineEvent> IEnumerable<ILineEvent>.GetEnumerator() => Events.GetEnumerator();

        internal static ILineEvents Empty()
        {
            return new LineEventCollection
            {
                Events = new List<LineEvent>()
            };
        }
    }
}
