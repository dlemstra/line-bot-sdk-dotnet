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

namespace Line.RichMenu
{
    /// <summary>
    /// Define the coordinates and size of tappable area.
    /// </summary>
    public class RichMenuArea
    {
        private RichMenuBounds _richMenuBounds;
        private ITemplateAction _action;

        /// <summary>
        /// Gets or sets object describing the boundaries of the area in pixels.
        /// </summary>
        [JsonProperty("bounds")]
        public RichMenuBounds RichMenuBounds
        {
            get => _richMenuBounds;
            set
            {
                if (value == null)
                    throw new InvalidOperationException("The bounds cannot be null.");

                _richMenuBounds = value;
            }
        }

        /// <summary>
        /// Gets or sets action performed when the area is tapped.
        /// </summary>
        [JsonProperty("action")]
        public ITemplateAction Action
        {
            get => _action;
            set
            {
                if (value == null)
                    throw new InvalidOperationException("The action cannot be null.");

                _action = value;
            }
        }
    }
}