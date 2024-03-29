﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a template message.
    /// </summary>
    public sealed class TemplateMessage : ISendMessage
    {
        private string? _alternativeText;
        private ITemplate? _template;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateMessage"/> class.
        /// </summary>
        public TemplateMessage()
        {
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        MessageType ISendMessage.Type
            => MessageType.Template;

        /// <summary>
        /// Gets or sets the alternative text for devices that do not support this type of message.
        /// <para>Max: 400 characters.</para>
        /// </summary>
        [JsonProperty("altText")]
        public string? AlternativeText
        {
            get => _alternativeText;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The alternative text cannot be null or whitespace.");

                if (value.Length > 400)
                    throw new InvalidOperationException("The alternative text cannot be longer than 400 characters.");

                _alternativeText = value;
            }
        }

        /// <summary>
        /// Gets or sets the template of the template message.
        /// </summary>
        [JsonProperty("template")]
        public ITemplate? Template
        {
            get => _template;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The template cannot be null.");

                if (IsInvalidTemplate(value))
                    throw new InvalidOperationException("The template type is invalid.");

                _template = value;
            }
        }

        void ISendMessage.Validate()
        {
            if (_alternativeText is null)
                throw new InvalidOperationException("The alternative text cannot be null.");

            if (_template is null)
                throw new InvalidOperationException("The template cannot be null.");

            _template.Validate();
        }

        private static bool IsInvalidTemplate(ITemplate value)
        {
            if (value is ButtonsTemplate)
                return false;

            if (value is CarouselTemplate)
                return false;

            if (value is ConfirmTemplate)
                return false;

            if (value is ImageCarouselTemplate)
                return false;

            return true;
        }
    }
}
