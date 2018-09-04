﻿// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
    /// Rich menu id response object. This object is returned when you get a rich menu or get a list of rich menus.
    /// </summary>
    internal sealed class RichMenuIdResponse
    {
        /// <summary>
        /// Gets or sets the rich menu ID.
        /// </summary>
        [JsonProperty("richMenuId")]
        public string RichMenuId { get; set; }
    }
}
