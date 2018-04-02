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
    /// Encapsulates a text message.
    /// </summary>
    public sealed class TextMessage : ITextMessage
    {
        private string _text;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        private MessageType _type = MessageType.Text;

#pragma warning restore 0414

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessage"/> class.
        /// </summary>
        public TextMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessage"/> class.
        /// </summary>
        /// <param name="text">
        /// The text of the message.
        /// <para>Max: 2000 characters</para>
        /// </param>
        public TextMessage(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Gets or sets the text of the message.
        /// <para>Max: 2000 characters</para>
        /// </summary>
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

                if (value.Length > 2000)
                    throw new InvalidOperationException("The text cannot be longer than 2000 characters.");

                _text = value;
            }
        }
    }
}
