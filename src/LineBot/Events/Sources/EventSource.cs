// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    internal sealed class EventSource : IEventSource
    {
        [JsonProperty("groupId")]
        private string? _groupId = null;

        [JsonProperty("roomId")]
        private string? _roomId = null;

        [JsonProperty("userId")]
        private string? _userId = null;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<EventSourceType>))]
        public EventSourceType SourceType { get; set; }

        public IGroup? Group
        {
            get
            {
                if (_groupId is null)
                    return null;

                return new Source(_groupId);
            }
        }

        public IRoom? Room
        {
            get
            {
                if (_roomId is null)
                    return null;

                return new Source(_roomId);
            }
        }

        public IUser? User
        {
            get
            {
                if (_userId is null)
                    return null;

                return new Source(_userId);
            }
        }
    }
}
