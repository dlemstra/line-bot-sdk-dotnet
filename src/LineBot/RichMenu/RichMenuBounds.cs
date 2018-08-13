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

using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Object describing the boundaries of the area in pixels.
    /// </summary>
    public class RichMenuBounds
    {
        /// <summary>
        /// Gets or sets horizontal position relative to the top-left corner of the area.
        /// </summary>
        [JsonProperty("x")]
        public int X { get; set; }

        /// <summary>
        /// Gets or sets vertical position relative to the top-left corner of the area.
        /// </summary>
        [JsonProperty("y")]
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets width of the area.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets height of the area.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }
    }
}