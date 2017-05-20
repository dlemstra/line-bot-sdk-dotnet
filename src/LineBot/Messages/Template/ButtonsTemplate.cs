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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a buttons template.
    /// </summary>
    public sealed class ButtonsTemplate : IButtonsTemplate
    {
        private Uri _thumbnailUrl;
        private string _title;
        private string _text;
        private IEnumerable<ITemplateAction> _actions;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<TemplateType>))]
        private TemplateType _type = TemplateType.Buttons;

#pragma warning restore 0414

        /// <summary>
        /// Gets or sets the image url for the thumbnail.
        /// </summary>
        /// <remarks>
        /// Protocol: HTTPS<para/>
        /// Format: JPEG or PNG<para/>
        /// Max url length: 1000 characters<para/>
        /// Aspect ratio: 1:1.51<para/>
        /// Max width: 1024px<para/>
        /// Max size: 1 MB
        /// </remarks>
        [JsonProperty("thumbnailImageUrl")]
        public Uri ThumbnailUrl
        {
            get
            {
                return _thumbnailUrl;
            }

            set
            {
                if (value != null)
                {
                    if (!"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("The thumbnail url should use the https scheme.");

                    if (value.ToString().Length > 1000)
                        throw new InvalidOperationException("The thumbnail url cannot be longer than 1000 characters.");
                }

                _thumbnailUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <remarks>Max: 400 characters</remarks>
        [JsonProperty("title")]
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (value != null && value.Length > 400)
                    throw new InvalidOperationException("The title cannot be longer than 400 characters.");

                _title = value;
            }
        }

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        /// <remarks>
        /// Max: 160 characters (no image or title)<para/>
        /// Max: 60 characters (message with an image or title)
        /// </remarks>
        [JsonProperty("text")]
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The text cannot be null or whitespace.");

                if (value.Length > 160)
                    throw new InvalidOperationException("The text cannot be longer than 160 characters.");

                if ((ThumbnailUrl != null || Title != null) && value.Length > 60)
                    throw new InvalidOperationException("The text cannot be longer than 60 characters when the thumnail url or title are set.");

                _text = value;
            }
        }

        /// <summary>
        /// Gets or sets the actions when tapped.
        /// </summary>
        /// <remarks>
        /// Max: 4
        /// </remarks>
        [JsonProperty("actions")]
        public IEnumerable<ITemplateAction> Actions
        {
            get
            {
                return _actions;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The actions cannot be null.");

                if (value.Count() > 4)
                    throw new InvalidOperationException("The maximum number of actions is 4.");

                foreach (var action in value)
                {
                    var interfaces = action.GetType().GetTypeInfo().ImplementedInterfaces;
                    if (!interfaces.Contains(typeof(IPostbackAction)) &&
                        !interfaces.Contains(typeof(IMessageAction)) &&
                        !interfaces.Contains(typeof(IUriAction)))
                        throw new InvalidOperationException("The template action type is invalid.");
                }

                _actions = value;
            }
        }
    }
}
