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
    /// Define the coordinates and size of tappable area.
    /// </summary>
    public class RichMenuArea : IRichMenuArea
    {
        private ITemplateAction _action;
        private IRichMenuBounds _bounds;

        /// <summary>
        /// Gets or sets the action performed when the area is tapped.
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

        /// <summary>
        /// Gets or sets the objects describing the boundaries of the area in pixels.
        /// </summary>
        [JsonProperty("bounds")]
        public IRichMenuBounds Bounds
        {
            get => _bounds;
            set
            {
                if (value == null)
                    throw new InvalidOperationException("The bounds cannot be null.");

                _bounds = value;
            }
        }

        internal static RichMenuArea Convert(IRichMenuArea richMenuArea)
        {
            if (richMenuArea.Action == null)
                throw new InvalidOperationException("The action cannot be null.");

            if (richMenuArea.Bounds == null)
                throw new InvalidOperationException("The bounds cannot be null.");

            if (richMenuArea is RichMenuArea area)
                return area;

            richMenuArea.Action.Validate();

            return new RichMenuArea()
            {
                Action = richMenuArea.Action,
                Bounds = RichMenuBounds.Convert(richMenuArea.Bounds)
            };
        }
    }
}