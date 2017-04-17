// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

using System;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class LineEvent : ILineEvent, IBeacon, IMessage, IPostback
    {
        [JsonProperty("beacon")]
        private Beacon _beacon = null;

        [JsonProperty("message")]
        private Message _message = null;

        [JsonProperty("postback")]
        private Postback _postback = null;

        [JsonProperty("replyToken")]
        private string _replyToken = null;

        [JsonProperty("source")]
        private EventSource _source = null;

        public IBeacon Beacon
        {
            get
            {
                if (EventType != LineEventType.Beacon || _beacon == null)
                    return null;

                return this;
            }
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<LineEventType>))]
        public LineEventType EventType { get; set; }

        public IMessage Message
        {
            get
            {
                if (EventType != LineEventType.Message || _message == null)
                    return null;

                return this;
            }
        }

        public IPostback Postback
        {
            get
            {
                if (EventType != LineEventType.Postback || _postback == null)
                    return null;

                return this;
            }
        }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        BeaconType IBeacon.BeaconType => _beacon.BeaconType;

        string IBeacon.Hwid => _beacon.Hwid;

        IEventSource ILineEvent.Source => _source;

        MessageType IMessage.MessageType => _message.MessageType;

        string IPostback.Data => _postback.Data;

        string IReplyToken.ReplyToken
        {
            get
            {
                if (EventType == LineEventType.Beacon ||
                    EventType == LineEventType.Follow ||
                    EventType == LineEventType.Join ||
                    EventType == LineEventType.Postback)
                    return _replyToken;

                return null;
            }
        }
    }
}
