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
    /// Encapsulates a base class for imagemap actions.
    /// </summary>
    public abstract class ImagemapAction
    {
        private ImagemapArea? _area;
        private string? _label;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ImagemapActionType>))]
        private ImagemapActionType _type;

        internal ImagemapAction(ImagemapActionType type)
        {
            _type = type;
        }

        /// <summary>
        /// Gets or sets the tappable area.
        /// </summary>
        [JsonProperty("area")]
        public ImagemapArea? Area
        {
            get
            {
                return _area;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The area cannot be null.");

                _area = value;
            }
        }

        /// <summary>
        /// Gets or sets the label.
        /// <para>Max: 50 characters.</para>
        /// </summary>
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string? Label
        {
            get
            {
                return _label;
            }

            set
            {
                if (value?.Length > 50)
                    throw new InvalidOperationException("The label cannot be longer than 50 characters.");

                _label = value;
            }
        }

        internal virtual void Validate()
        {
            if (_area == null)
                throw new InvalidOperationException("The area cannot be null.");
        }
    }
}
