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

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a template message.
    /// </summary>
    public sealed class TemplateMessage : ISendMessage
    {
#pragma warning disable 0414 // Suppress value is never used.
        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        private readonly MessageType _type = MessageType.Template;
#pragma warning restore 0414

        private string _alternativeText;
        private ITemplate _template;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateMessage"/> class.
        /// </summary>
        public TemplateMessage()
        {
        }

        /// <summary>
        /// Gets or sets the alternative text for devices that do not support this type of message.
        /// <para>Max: 400 characters.</para>
        /// </summary>
        [JsonProperty("altText")]
        public string AlternativeText
        {
            get
            {
                return _alternativeText;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
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
        public ITemplate Template
        {
            get
            {
                return _template;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The template cannot be null.");

                if (IsInvalidTemplate(value))
                    throw new InvalidOperationException("The template type is invalid.");

                _template = value;
            }
        }

        void ISendMessage.Validate()
        {
            if (_alternativeText == null)
                throw new InvalidOperationException("The alternative text cannot be null.");

            if (_template == null)
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
