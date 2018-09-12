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
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Line
{
    /// <summary>
    /// Rich menu response object. This object is returned when you get rich menu or get a list of rich menus.
    /// </summary>
    public class RichMenuResponse : RichMenu, IRichMenuResponse
    {
        /// <summary>
        /// Gets or sets the rich menu ID.
        /// </summary>
        [JsonProperty("richMenuId")]
        public string RichMenuId { get; set; }

        public static RichMenuResponse ConvertFromJson(string jsonString)
        {
            var richMenuIdJToken = JObject.Parse(jsonString)["richMenuId"];

            if (richMenuIdJToken == null)
                return null;

            var richMenuId = richMenuIdJToken.ToObject<string>();

            var result = new RichMenuResponse
            {
                RichMenuId = richMenuId
            };

            return result;
        }
    }
}
