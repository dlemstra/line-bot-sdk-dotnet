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
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates an audio message.
    /// </summary>
    public sealed class AudioMessage : IAudioMessage
    {
        private Uri _url;
        private int _duration;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        private MessageType _type = MessageType.Audio;

#pragma warning restore 0414

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMessage"/> class.
        /// </summary>
        public AudioMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMessage"/> class.
        /// </summary>
        /// <param name="url">The url of the audio file.</param>
        /// <param name="duration">The length of audio file in milliseconds.</param>
        public AudioMessage(string url, int duration)
            : this(new Uri(url), duration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMessage"/> class.
        /// </summary>
        /// <param name="url">The url of the audio file.</param>
        /// <param name="duration">The length of audio file in milliseconds.</param>
        public AudioMessage(Uri url, int duration)
        {
            Url = url;
            Duration = duration;
        }

        /// <summary>
        /// Gets or sets the url of the audio file.
        /// </summary>
        /// <remarks>
        /// Protocol: HTTPS<para/>
        /// Format: M4A<para/>
        /// Max url length: 1000 characters<para/>
        /// Max duration: 1 minute<para/>
        /// Max size: 10 MB
        /// </remarks>
        [JsonProperty("originalContentUrl")]
        public Uri Url
        {
            get
            {
                return _url;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The url cannot be null.");

                if (!"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("The url should use the https scheme.");

                if (value.ToString().Length > 1000)
                    throw new InvalidOperationException("The url cannot be longer than 1000 characters.");

                _url = value;
            }
        }

        /// <summary>
        /// Gets or sets the length of audio file in milliseconds.
        /// </summary>
        [JsonProperty("duration")]
        public int Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                if (value < 1)
                    throw new InvalidOperationException("The duration should be at least 1 millisecond.");

                if (value >= 60000)
                    throw new InvalidOperationException("The duration cannot be longer than 1 minute.");

                _duration = value;
            }
        }
    }
}
