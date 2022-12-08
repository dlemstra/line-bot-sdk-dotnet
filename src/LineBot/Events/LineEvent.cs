// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class LineEvent : ILineEvent, IBeacon, IMessage, IPostback
    {
        [JsonProperty("beacon")]
        private Beacon? _beacon = null;

        [JsonProperty("message")]
        private Message? _message = null;

        [JsonProperty("postback")]
        private Postback? _postback = null;

        [JsonProperty("replyToken")]
        private string? _replyToken = null;

        [JsonProperty("source")]
        private EventSource? _source = null;

        public IBeacon? Beacon
        {
            get
            {
                if (EventType != LineEventType.Beacon || _beacon is null)
                    return null;

                return this;
            }
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<LineEventType>))]
        public LineEventType EventType { get; set; }

        public IMessage? Message
        {
            get
            {
                if (EventType != LineEventType.Message || _message is null)
                    return null;

                return this;
            }
        }

        public IPostback? Postback
        {
            get
            {
                if (EventType != LineEventType.Postback || _postback is null)
                    return null;

                return this;
            }
        }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        BeaconType IBeacon.BeaconType => _beacon!.BeaconType!;

        string IBeacon.Hwid => _beacon!.Hwid!;

        IEventSource ILineEvent.Source => _source!;

        string IMessage.Id => _message!.Id;

        ILocation IMessage.Location => _message!.Location!;

        MessageType IMessage.MessageType => _message!.MessageType;

        ISticker IMessage.Sticker => _message!.Sticker!;

        string IMessage.Text => _message!.Text;

        string IPostback.Data => _postback!.Data;

        string? IReplyToken.ReplyToken
        {
            get
            {
                if (EventType == LineEventType.Beacon ||
                    EventType == LineEventType.Follow ||
                    EventType == LineEventType.Join ||
                    EventType == LineEventType.Message ||
                    EventType == LineEventType.Postback)
                    return _replyToken;

                return null;
            }
        }
    }
}
