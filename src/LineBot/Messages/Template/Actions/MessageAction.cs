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
    /// Encapsulates a template message action.
    /// </summary>
    public sealed class MessageAction : IMessageAction
    {
        private string _label;
        private string _text;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<TemplateActionType>))]
        private readonly TemplateActionType _type = TemplateActionType.Message;

#pragma warning restore 0414

        /// <summary>
        /// Gets or sets the label.
        /// <para>Max: 20 characters.</para>
        /// </summary>
        [JsonProperty("label")]
        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The label cannot be null or whitespace.");

                if (value.Length > 20)
                    throw new InvalidOperationException("The label cannot be longer than 20 characters.");

                _label = value;
            }
        }

        /// <summary>
        /// Gets or sets the text sent when the action is performed.
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

                if (value.Length > 300)
                    throw new InvalidOperationException("The text cannot be longer than 300 characters.");

                _text = value;
            }
        }

        internal static MessageAction Convert(IMessageAction action)
        {
            if (action.Label == null)
                throw new InvalidOperationException("The label cannot be null.");

            if (action.Text == null)
                throw new InvalidOperationException("The text cannot be null.");

            if (action is MessageAction messageAction)
                return messageAction;

            return new MessageAction()
            {
                Label = action.Label,
                Text = action.Text
            };
        }
    }
}
