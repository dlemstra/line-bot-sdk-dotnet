// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

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
