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
    /// Rich menu object.
    /// </summary>
    public class RichMenuRequest : IRichMenuRequest
    {
        private RichMenuArea[] _areas;
        private string _chatBarText;
        private string _name;
        private RichMenuSize _size;

        /// <summary>
        /// Gets or sets the object which contains the width and height of the rich menu displayed in the chat.
        /// Rich menu images must be one of the following sizes: 2500x1686px or 2500x843px.
        /// </summary>
        [JsonProperty("size")]
        public RichMenuSize Size
        {
            get => _size;
            set
            {
                if (value == null)
                    throw new InvalidOperationException("The size cannot be null.");

                _size = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the rich menu should be displayed by default.
        /// </summary>
        [JsonProperty("selected")]
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the name of the rich menu. This value can be used to help manage your rich menus and is not displayed to users.
        /// Max: 300 characters.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The name cannot be null or whitespace.");

                if (value.Length > 300)
                    throw new InvalidOperationException("The name cannot be longer than 300 characters.");

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the text displayed in the chat bar.
        /// Max: 14 characters.
        /// </summary>
        [JsonProperty("chatBarText")]
        public string ChatBarText
        {
            get => _chatBarText;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The chat bar text cannot be null or whitespace.");

                if (value.Length > 14)
                    throw new InvalidOperationException("The chat bar text cannot be longer than 14 characters.");

                _chatBarText = value;
            }
        }

        /// <summary>
        /// Gets or sets the array of area objects which define the coordinates and size of tappable areas.
        /// Max: 20 area objects.
        /// </summary>
        [JsonProperty("areas")]
        public RichMenuArea[] Areas
        {
            get => _areas;
            set
            {
                if (value == null)
                    throw new InvalidOperationException("The areas cannot be null.");

                if (value.Length > 20)
                    throw new InvalidOperationException("The maximum number of areas is 20.");

                _areas = value;
            }
        }

        /// <summary>
        /// Convert for richmenuRequest.
        /// </summary>
        /// <param name="richMenuRequest">richMenuRequest object.</param>
        /// <returns>RichMenuRequest's instance.</returns>
        public static RichMenuRequest Convert(IRichMenuRequest richMenuRequest)
        {
            if (richMenuRequest is RichMenuRequest request)
            {
                return request;
            }

            return new RichMenuRequest();
        }
    }
}
