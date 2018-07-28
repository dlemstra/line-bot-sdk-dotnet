// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates an imagemap message.
    /// </summary>
    public sealed class ImagemapMessage : IImagemapMessage
    {
        private Uri _baseUrl;
        private string _alternateText;
        private ImagemapSize _baseSize;
        private IEnumerable<ImagemapAction> _actions;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        private readonly MessageType _type = MessageType.Imagemap;

#pragma warning restore 0414

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapMessage"/> class.
        /// </summary>
        public ImagemapMessage()
        {
        }

        /// <summary>
        /// Gets or sets the base url of the image.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG or PNG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max dimensions: 1024 x (max 1024).</para>
        /// <para>Max size: 1 MB.</para>
        /// </summary>
        [JsonProperty("baseUrl")]
        public Uri BaseUrl
        {
            get
            {
                return _baseUrl;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The base url cannot be null.");

                if (!"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("The base url should use the https scheme.");

                if (value.ToString().Length > 1000)
                    throw new InvalidOperationException("The base url cannot be longer than 1000 characters.");

                _baseUrl = value;
            }
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
        /// Gets or sets the size of the base image (Width must be 1040).
        /// </summary>
        [JsonProperty("baseSize")]
        public ImagemapSize BaseSize
        {
            get
            {
                return _baseSize;
            }

            set
            {
                _baseSize = value ?? throw new InvalidOperationException("The base size cannot be null.");
            }
        }

        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        [JsonProperty("actions")]
        public IEnumerable<ImagemapAction> Actions
        {
            get
            {
                return _actions;
            }

            set
            {
                _actions = value ?? throw new InvalidOperationException("The actions cannot be null.");
            }
        }

        IEnumerable<IImagemapAction> IImagemapMessage.Actions => Actions;

        IImagemapSize IImagemapMessage.BaseSize => BaseSize;

        internal static ImagemapMessage Convert(IImagemapMessage message)
        {
            if (message.BaseUrl == null)
                throw new InvalidOperationException("The base url cannot be null.");

            if (message.AlternativeText == null)
                throw new InvalidOperationException("The alternative text cannot be null.");

            if (message.BaseSize == null)
                throw new InvalidOperationException("The base size cannot be null.");

            if (message.Actions == null)
                throw new InvalidOperationException("The actions cannot be null.");

            if (!(message is ImagemapMessage imagemapMessage))
            {
                imagemapMessage = new ImagemapMessage()
                {
                    BaseUrl = message.BaseUrl,
                    AlternativeText = message.AlternativeText,
                };
            }

            imagemapMessage.BaseSize = ImagemapSize.Convert(message.BaseSize);
            imagemapMessage.Actions = ConvertActions(message.Actions);

            return imagemapMessage;
        }

        private static IEnumerable<ImagemapAction> ConvertActions(IEnumerable<IImagemapAction> actions)
        {
            var imagemapActions = actions.ToArray();
            var result = new ImagemapAction[imagemapActions.Length];
            for (int i = 0; i < imagemapActions.Length; i++)
            {
                result[i] = ImagemapAction.Convert(imagemapActions[i]);
            }

            return result;
        }
    }
}
