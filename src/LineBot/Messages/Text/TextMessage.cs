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

using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a text message.
    /// </summary>
    public sealed class TextMessage : ITextMessage
    {
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

        internal TextMessage(ITextMessage self)
        {
            Text = self.Text;
        }

        /// <summary>
        /// Gets or sets the text of the message.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
