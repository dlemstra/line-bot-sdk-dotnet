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
    /// RichMenuSize object which contains the width and height of the rich menu displayed in the chat.
    /// Rich menu images must be one of the following sizes: 2500x1686px or 2500x843px.
    /// </summary>
    public class RichMenuSize
    {
        private int _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="RichMenuSize"/> class.
        /// </summary>
        public RichMenuSize()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RichMenuSize"/> class.
        /// </summary>
        /// <param name="height">The height of the rich menu. Possible values: 1686, 843.</param>
        public RichMenuSize(int height)
        {
            Height = height;
        }

        /// <summary>
        /// Gets the width of the rich menu. Will always be 2500.
        /// </summary>
        [JsonProperty("width")]
        public int Width => 2500;

        /// <summary>
        /// Gets or sets the height of the rich menu. Possible values: 1686, 843.
        /// </summary>
        [JsonProperty("height")]
        public int Height
        {
            get => _height;
            set
            {
                if (value != 843 && value != 1686)
                    throw new InvalidOperationException("The possible height values are: 1686, 843.");

                _height = value;
            }
        }

        internal void Validate()
        {
            if (_height == 0)
                throw new InvalidOperationException("The height is not set.");
        }
    }
}