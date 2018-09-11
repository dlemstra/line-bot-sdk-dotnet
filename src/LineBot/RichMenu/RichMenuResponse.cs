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
    /// Rich menu response object. This object is returned when you get rich menu or get a list of rich menus.
    /// </summary>
    public class RichMenuResponse : RichMenu, IRichMenuResponse
    {
        public RichMenuResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RichMenuResponse"/> class.
        /// This constructor is required for the JSON deserializer to be able
        /// to identify concrete classes to use when deserializing the interface properties.
        /// </summary>
        /// <param name="areas">areas.</param>
        /// <param name="richMenuSize">rich menu size.</param>
        [JsonConstructor]
        public RichMenuResponse(IRichMenuArea[] areas, IRichMenuSize richMenuSize)
            : base(areas, richMenuSize)
        {
        }

        /// <summary>
        /// Gets or sets the rich menu ID.
        /// </summary>
        [JsonProperty("richMenuId")]
        public string RichMenuId { get; set; }

        internal static RichMenuResponse Convert(IRichMenuResponse menu)
        {
            if (menu.Areas == null)
                throw new InvalidOperationException("The areas cannot be null.");

            if (menu.ChatBarText == null)
                throw new InvalidOperationException("The chat bar text cannot be null.");

            if (menu.Name == null)
                throw new InvalidOperationException("The name cannot be null.");

            if (menu.Size == null)
                throw new InvalidOperationException("The size cannot be null.");

            if (menu is RichMenuResponse richMenuResponse)
            {
                return richMenuResponse;
            }

            return new RichMenuResponse()
            {
                RichMenuId = menu.RichMenuId,
                Areas = ConvertAreas(menu.Areas),
                ChatBarText = menu.ChatBarText,
                Name = menu.Name,
                Size = RichMenuSize.Convert(menu.Size)
            };
        }

        private static RichMenuArea[] ConvertAreas(IRichMenuArea[] areas)
        {
            var result = new RichMenuArea[areas.Length];
            for (int i = 0; i < areas.Length; i++)
            {
                result[i] = RichMenuArea.Convert(areas[i]);
            }

            return result;
        }
    }
}
