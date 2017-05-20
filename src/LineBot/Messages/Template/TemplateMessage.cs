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
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a template message.
    /// </summary>
    public sealed class TemplateMessage : ITemplateMessage
    {
        private string _alternateText;
        private ITemplate _template;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        private MessageType _type = MessageType.Template;

#pragma warning restore 0414

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateMessage"/> class.
        /// </summary>
        public TemplateMessage()
        {
        }

        /// <summary>
        /// Gets or sets the alternative text for devices that do not support this type of message.
        /// </summary>
        /// <remarks>Max: 400 characters</remarks>
        [JsonProperty("altText")]
        public string AlternativeText
        {
            get
            {
                return _alternateText;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The alternative text cannot be null or whitespace.");

                if (value.Length > 400)
                    throw new InvalidOperationException("The alternative text cannot be longer than 400 characters.");

                _alternateText = value;
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

                var interfaces = value.GetType().GetTypeInfo().ImplementedInterfaces;
                if (!interfaces.Contains(typeof(IButtonsTemplate)) &&
                    !interfaces.Contains(typeof(IConfirmTemplate)) &&
                    !interfaces.Contains(typeof(ICarouselTemplate)))
                    throw new InvalidOperationException("The template type is invalid.");

                _template = value;
            }
        }
    }
}
