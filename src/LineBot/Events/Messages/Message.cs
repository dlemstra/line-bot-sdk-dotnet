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
    internal sealed class Message : ILocation, ISticker
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }

        public ILocation Location
        {
            get
            {
                if (MessageType != MessageType.Location)
                    return null;

                return this;
            }
        }

        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        public MessageType MessageType { get; set; }

        [JsonProperty("packageid")]
        public string PackageId { get; set; }

        public ISticker Sticker
        {
            get
            {
                if (MessageType != MessageType.Sticker)
                    return null;

                return this;
            }
        }

        [JsonProperty("stickerId")]
        public string StickerId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
