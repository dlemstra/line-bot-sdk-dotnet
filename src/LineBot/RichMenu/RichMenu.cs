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
using System.Linq;
using Newtonsoft.Json;

namespace Line.RichMenu
{
    /// <summary>
    /// Rich Menu object. Without the rich menu ID.
    /// </summary>
    public class RichMenu
    {
        private RichMenuArea[] _richMenuAreas;
        private string _chatBarText;
        private string _name;

        /// <summary>
        /// Gets or sets object which contains the width and height of the rich menu displayed in the chat.
        /// Rich menu images must be one of the following sizes: 2500x1686px or 2500x843px.
        /// </summary>
        [JsonProperty("size")]
        public RichMenuSize RichMenuSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether rich menu is selected.
        /// True to display the rich menu by default. Otherwise, false.
        /// </summary>
        [JsonProperty("selected")]
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets name of the rich menu. This value can be used to help manage your rich menus and is not displayed to users.
        /// Max: 300 characters.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length > 300)
                    throw new InvalidOperationException("The name cannot be longer than 300 characters.");

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets text displayed in the chat bar
        /// Max: 14 characters.
        /// </summary>
        [JsonProperty("chatBarText")]
        public string ChatBarText
        {
            get => _chatBarText;
            set
            {
                if (value.Length > 14)
                    throw new InvalidOperationException("The chat bar text cannot be longer than 14 characters.");

                _chatBarText = value;
            }
        }

        /// <summary>
        /// Gets or sets array of area objects which define the coordinates and size of tappable areas
        /// Max: 20 area objects.
        /// </summary>
        [JsonProperty("areas")]
        public RichMenuArea[] RichMenuAreas
        {
            get => _richMenuAreas;
            set
            {
                if (value.Count() > 20)
                    throw new InvalidOperationException("The maximum number of areas is 20.");

                _richMenuAreas = value;
            }
        }
    }
}
