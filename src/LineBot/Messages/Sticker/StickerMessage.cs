// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a sticker message.
    /// </summary>
    public sealed class StickerMessage : ISendMessage
    {
        private string? _packageId;
        private string? _stickerId;

        /// <summary>
        /// Initializes a new instance of the <see cref="StickerMessage"/> class.
        /// </summary>
        public StickerMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickerMessage"/> class.
        /// </summary>
        /// <param name="packageId">The id of the package.</param>
        /// <param name="stickerId">The id of the sticker.</param>
        public StickerMessage(string packageId, string stickerId)
        {
            PackageId = packageId;
            StickerId = stickerId;
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]

        MessageType ISendMessage.Type
            => MessageType.Sticker;

        /// <summary>
        /// Gets or sets the id of the package.
        /// </summary>
        [JsonProperty("packageId")]
        public string? PackageId
        {
            get => _packageId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The package id cannot be null or whitespace.");

                _packageId = value;
            }
        }

        /// <summary>
        /// Gets or sets the id of the sticker.
        /// </summary>
        [JsonProperty("stickerId")]
        public string? StickerId
        {
            get => _stickerId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The sticker id cannot be null or whitespace.");

                _stickerId = value;
            }
        }

        void ISendMessage.Validate()
        {
            if (_packageId is null)
                throw new InvalidOperationException("The package id cannot be null.");

            if (_stickerId is null)
                throw new InvalidOperationException("The sticker id cannot be null.");
        }
    }
}
