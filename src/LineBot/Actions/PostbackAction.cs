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
    /// Encapsulates a postback action.
    /// </summary>
    public sealed class PostbackAction : IAction
    {
#pragma warning disable 0414 // Suppress value is never used.
        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ActionType>))]
        private readonly ActionType _type = ActionType.Postback;
#pragma warning restore 0414

        private string _label;
        private string _data;
        private string _text;

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
        /// Gets or sets the string returned via webhook in the postback.data property of the <see cref="IPostback"/> event.
        /// <para>Max: 300 characters.</para>
        /// </summary>
        [JsonProperty("data")]
        public string Data
        {
            get
            {
                return _data;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The data cannot be null or whitespace.");

                if (value.Length > 300)
                    throw new InvalidOperationException("The data cannot be longer than 300 characters.");

                _data = value;
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
                if (value != null && value.Length > 300)
                    throw new InvalidOperationException("The text cannot be longer than 300 characters.");

                _text = value;
            }
        }

        void IAction.Validate()
        {
            if (_label == null)
                throw new InvalidOperationException("The label cannot be null.");

            if (_data == null)
                throw new InvalidOperationException("The data cannot be null.");
        }
    }
}
