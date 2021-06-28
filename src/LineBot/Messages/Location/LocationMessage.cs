// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a location message.
    /// </summary>
    public sealed class LocationMessage : ISendMessage
    {
        private string? _title;
        private string? _address;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationMessage"/> class.
        /// </summary>
        public LocationMessage()
        {
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        MessageType ISendMessage.Type
            => MessageType.Location;

        /// <summary>
        /// Gets or sets the title.
        /// <para>Max: 100 characters.</para>
        /// </summary>
        [JsonProperty("title")]
        public string? Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The title cannot be null or whitespace.");

                if (value.Length > 100)
                    throw new InvalidOperationException("The title cannot be longer than 100 characters.");

                _title = value;
            }
        }

        /// <summary>
        /// Gets or sets the address.
        /// <para>Max: 100 characters.</para>
        /// </summary>
        [JsonProperty("address")]
        public string? Address
        {
            get
            {
                return _address;
            }

            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The address cannot be null or whitespace.");

                if (value.Length > 100)
                    throw new InvalidOperationException("The address cannot be longer than 100 characters.");

                _address = value;
            }
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }

        void ISendMessage.Validate()
        {
            if (_title is null)
                throw new InvalidOperationException("The title cannot be null.");

            if (_address is null)
                throw new InvalidOperationException("The address cannot be null.");
        }
    }
}
