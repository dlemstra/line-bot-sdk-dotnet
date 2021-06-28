// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a message action.
    /// </summary>
    public sealed class MessageAction : IAction
    {
        private string? _label;
        private string? _text;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ActionType>))]
        ActionType IAction.Type
            => ActionType.Message;

        /// <summary>
        /// Gets or sets the label.
        /// <para>Max: 20 characters.</para>
        /// </summary>
        [JsonProperty("label")]
        public string? Label
        {
            get => _label;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The label cannot be null or whitespace.");

                if (value.Length > 20)
                    throw new InvalidOperationException("The label cannot be longer than 20 characters.");

                _label = value;
            }
        }

        /// <summary>
        /// Gets or sets the text sent when the action is performed.
        /// </summary>
        [JsonProperty("text")]
        public string? Text
        {
            get => _text;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The text cannot be null or whitespace.");

                if (value.Length > 300)
                    throw new InvalidOperationException("The text cannot be longer than 300 characters.");

                _text = value;
            }
        }

        void IAction.Validate()
        {
            if (_label is null)
                throw new InvalidOperationException("The label cannot be null.");

            if (_text is null)
                throw new InvalidOperationException("The text cannot be null.");
        }
    }
}
