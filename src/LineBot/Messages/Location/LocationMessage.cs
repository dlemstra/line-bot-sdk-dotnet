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
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a location message.
    /// </summary>
    public sealed class LocationMessage : ILocationMessage
    {
        private string _title;
        private string _address;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        private MessageType _type = MessageType.Location;

#pragma warning restore 0414

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationMessage"/> class.
        /// </summary>
        public LocationMessage()
        {
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <remarks>
        /// Max: 100 characters
        /// </remarks>
        [JsonProperty("title")]
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (value == null || value.Length == 0)
                    throw new InvalidOperationException("The title cannot be null or empty.");

                if (value.Length > 100)
                    throw new InvalidOperationException("The title cannot be longer than 100 characters.");

                _title = value;
            }
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <remarks>
        /// Max: 100 characters
        /// </remarks>
        [JsonProperty("address")]
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                if (value == null || value.Length == 0)
                    throw new InvalidOperationException("The address cannot be null or empty.");

                if (value.Length > 100)
                    throw new InvalidOperationException("The address cannot be longer than 100 characters.");

                _address = value;
            }
        }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }
    }
}
