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
    /// Encapsulates a image carousel column.
    /// </summary>
    public sealed class ImageCarouselColumn
    {
        private Uri? _imageUrl;
        private IAction? _action;

        /// <summary>
        /// Gets or sets the image url for the image carousel.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG or PNG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Aspect ratio: 1:1.</para>
        /// <para>Max width: 1024px.</para>
        /// <para>Max size: 1 MB.</para>
        /// </summary>
        [JsonProperty("imageUrl")]
        public Uri? ImageUrl
        {
            get
            {
                return _imageUrl;
            }

            set
            {
                if (value != null)
                {
                    if (!"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("The image url should use the https scheme.");

                    if (value.ToString().Length > 1000)
                        throw new InvalidOperationException("The image url cannot be longer than 1000 characters.");
                }

                _imageUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the action when image is tapped.
        /// </summary>
        [JsonProperty("action")]
        public IAction? Action
        {
            get
            {
                return _action;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The action cannot be null.");

                value.CheckActionType();

                _action = value;
            }
        }

        internal void Validate()
        {
            if (_imageUrl == null)
                throw new InvalidOperationException("The image url cannot be null.");

            if (_action == null)
                throw new InvalidOperationException("The action cannot be null.");

            _action.Validate();
        }
    }
}
