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
    /// Define the coordinates and size of tappable area.
    /// </summary>
    public class RichMenuArea
    {
        private IAction _action;
        private RichMenuBounds _bounds;

        /// <summary>
        /// Gets or sets the action performed when the area is tapped.
        /// </summary>
        [JsonProperty("action")]
        [JsonConverter(typeof(IActionConverter))]
        public IAction Action
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
        public RichMenuBounds Bounds
        {
            get => _bounds;
            set
            {
                if (value == null)
                    throw new InvalidOperationException("The bounds cannot be null.");

                _bounds = value;
            }
        }

        internal void Validate()
        {
            if (_action == null)
                throw new InvalidOperationException("The action cannot be null.");

            if (_bounds == null)
                throw new InvalidOperationException("The bounds cannot be null.");

            _action.Validate();
            _bounds.Validate();
        }
    }
}