// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a text message.
    /// </summary>
    public sealed class TextMessage : ISendMessage
    {
        private string? _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessage"/> class.
        /// </summary>
        public TextMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessage"/> class.
        /// </summary>
        /// <param name="text">
        /// The text of the message.
        /// <para>Max: 2000 characters.</para>
        /// </param>
        public TextMessage(string text)
        {
            Text = text;
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        MessageType ISendMessage.Type
            => MessageType.Text;

        /// <summary>
        /// Gets or sets the text of the message.
        /// <para>Max: 2000 characters.</para>
        /// </summary>
        [JsonProperty("text")]
        public string? Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The text cannot be null or whitespace.");

                if (value.Length > 2000)
                    throw new InvalidOperationException("The text cannot be longer than 2000 characters.");

                _text = value;
            }
        }

        void ISendMessage.Validate()
        {
            if (_text is null)
                throw new InvalidOperationException("The text cannot be null.");
        }
    }
}
