// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    internal sealed class Message : ILocation, ISticker
    {
        [JsonProperty("address")]
        public string Address { get; set; } = default!;

        [JsonProperty("id")]
        public string Id { get; set; } = default!;

        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }

        public ILocation? Location
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
        public string PackageId { get; set; } = string.Empty;

        public ISticker? Sticker
        {
            get
            {
                if (MessageType != MessageType.Sticker)
                    return null;

                return this;
            }
        }

        [JsonProperty("stickerId")]
        public string StickerId { get; set; } = default!;

        [JsonProperty("text")]
        public string Text { get; set; } = default!;

        [JsonProperty("title")]
        public string Title { get; set; } = default!;
    }
}
