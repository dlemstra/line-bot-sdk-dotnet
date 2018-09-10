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
    /// Object describing the boundaries of the area in pixels.
    /// </summary>
    public class RichMenuBounds : IRichMenuBounds
    {
        private int _x;
        private int _y;
        private int _width;
        private int _height;

        /// <summary>
        /// Gets or sets the horizontal position relative to the top-left corner of the area.
        /// </summary>
        [JsonProperty("x")]
        public int X
        {
            get => _x;
            set
            {
                if (value > 2500)
                    throw new InvalidOperationException("The horizontal position cannot be bigger than 2500.");

                if (value < 0)
                    throw new InvalidOperationException("The horizontal position cannot be less than 0.");

                if (value + _width > 2500)
                    throw new InvalidOperationException("The horizontal postion and width will exceed the rich menu's max width.");

                _x = value;
            }
        }

        /// <summary>
        /// Gets or sets the vertical position relative to the top-left corner of the area.
        /// </summary>
        [JsonProperty("y")]
        public int Y
        {
            get => _y;
            set
            {
                if (value > 1686)
                    throw new InvalidOperationException("The vertical position cannot be bigger than 1686.");

                if (value < 0)
                    throw new InvalidOperationException("The vertical position cannot be less than 0.");

                if (value + _height > 1686)
                    throw new InvalidOperationException("The vertical postion and height will exceed the rich menu's max height.");

                _y = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the area.
        /// </summary>
        [JsonProperty("width")]
        public int Width
        {
            get => _width;
            set
            {
                if (value > 2500)
                    throw new InvalidOperationException("The width cannot be bigger than 2500.");

                if (value < 1)
                    throw new InvalidOperationException("The width cannot be less than 1.");

                if (value + _x > 2500)
                    throw new InvalidOperationException("The horizontal postion and width will exceed the rich menu's max width.");

                _width = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of the area.
        /// </summary>
        [JsonProperty("height")]
        public int Height
        {
            get => _height;
            set
            {
                if (value > 1686)
                    throw new InvalidOperationException("The height cannot be bigger than 1686.");

                if (value < 1)
                    throw new InvalidOperationException("The height cannot be less than 1.");

                if (value + _y > 1686)
                    throw new InvalidOperationException("The vertical postion and height will exceed the rich menu's max height.");

                _height = value;
            }
        }

        internal static RichMenuBounds Convert(IRichMenuBounds richMenuBounds)
        {
            if (richMenuBounds.Width == 0)
                throw new InvalidOperationException("The width is not set.");

            if (richMenuBounds.Height == 0)
                throw new InvalidOperationException("The height is not set.");

            if (richMenuBounds is RichMenuBounds bounds)
            {
                return bounds;
            }

            return new RichMenuBounds()
            {
                X = richMenuBounds.X,
                Y = richMenuBounds.Y,
                Width = richMenuBounds.Width,
                Height = richMenuBounds.Height,
            };
        }
    }
}