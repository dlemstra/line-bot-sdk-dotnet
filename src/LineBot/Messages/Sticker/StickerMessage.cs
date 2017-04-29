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
    /// Encapsulates a sticker message.
    /// </summary>
    public sealed class StickerMessage : IStickerMessage
    {
        private string _packageId;
        private string _stickerId;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        private MessageType _type = MessageType.Sticker;

#pragma warning restore 0414

        /// <summary>
        /// Initializes a new instance of the <see cref="StickerMessage"/> class.
        /// </summary>
        public StickerMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StickerMessage"/> class.
        /// </summary>
        /// <param name="packageId">The id of the package.</param>
        /// <param name="stickerId">The id of the sticker.</param>
        public StickerMessage(string packageId, string stickerId)
        {
            PackageId = packageId;
            StickerId = stickerId;
        }

        /// <summary>
        /// Gets or sets the id of the package.
        /// </summary>
        [JsonProperty("packageId")]
        public string PackageId
        {
            get
            {
                return _packageId;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The package id cannot be null or whitespace.");

                _packageId = value;
            }
        }

        /// <summary>
        /// Gets or sets the id of the sticker.
        /// </summary>
        [JsonProperty("stickerId")]
        public string StickerId
        {
            get
            {
                return _stickerId;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The sticker id cannot be null or whitespace.");

                _stickerId = value;
            }
        }
    }
}
