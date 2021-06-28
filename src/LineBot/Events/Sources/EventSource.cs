// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    internal sealed class EventSource : IEventSource, IGroup, IRoom, IUser
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

        string IGroup.Id => _groupId!;

        public IGroup? Group
        {
            get
            {
                if (SourceType != EventSourceType.Group)
                    return null;

                return this;
            }
        }

        string IRoom.Id => _roomId!;

        public IRoom? Room
        {
            get
            {
                if (SourceType != EventSourceType.Room)
                    return null;

                return this;
            }
        }

        string IUser.Id => _userId!;

        public IUser? User
        {
            get
            {
                if (SourceType != EventSourceType.User)
                    return null;

                return this;
            }
        }
    }
}
