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
    /// Encapsulates an image message.
    /// </summary>
    public sealed class ImageMessage : ISendMessage
    {
        private Uri? _url;
        private Uri? _previewUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMessage"/> class.
        /// </summary>
        public ImageMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMessage"/> class.
        /// </summary>
        /// <param name="url">
        /// The image url.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max dimensions: 1024 x 1024.</para>
        /// <para>Max size: 1 MB.</para>
        /// </param>
        /// <param name="previewUrl">The preview image url.</param>
        public ImageMessage(string url, string previewUrl)
            : this(new Uri(url), new Uri(previewUrl))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMessage"/> class.
        /// </summary>
        /// <param name="url">
        /// The image url.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max dimensions: 1024 x 1024.</para>
        /// <para>Max size: 1 MB.</para>
        /// </param>
        /// <param name="previewUrl">The preview image url.</param>
        public ImageMessage(Uri url, Uri previewUrl)
        {
            Url = url;
            PreviewUrl = previewUrl;
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        MessageType ISendMessage.Type
            => MessageType.Image;

        /// <summary>
        /// Gets or sets the image url.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max dimensions: 1024 x 1024.</para>
        /// <para>Max size: 1 MB.</para>
        /// </summary>
        [JsonProperty("originalContentUrl")]
        public Uri? Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = CheckUrl(value);
            }
        }

        /// <summary>
        /// Gets or sets the preview image url.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max dimensions: 240 x 240.</para>
        /// <para>Max size: 1 MB.</para>
        /// </summary>
        [JsonProperty("previewImageUrl")]
        public Uri? PreviewUrl
        {
            get
            {
                return _previewUrl;
            }

            set
            {
                _previewUrl = CheckUrl(value);
            }
        }

        void ISendMessage.Validate()
        {
            if (_url == null)
                throw new InvalidOperationException("The url cannot be null.");

            if (_previewUrl == null)
                throw new InvalidOperationException("The preview url cannot be null.");
        }

        private Uri CheckUrl(Uri? value)
        {
            if (value == null)
                throw new InvalidOperationException("The url cannot be null.");

            if (!"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("The url should use the https scheme.");

            if (value.ToString().Length > 1000)
                throw new InvalidOperationException("The url cannot be longer than 1000 characters.");

            return value;
        }
    }
}
