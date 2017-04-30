// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
    /// Encapsulates an imagemap area.
    /// </summary>
    public class ImagemapArea : ImagemapSize, IImagemapArea
    {
        private int _x;
        private int _y;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapArea"/> class.
        /// </summary>
        public ImagemapArea()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapArea"/> class.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ImagemapArea(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets or sets the x position.
        /// </summary>
        [JsonProperty("x")]
        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                if (value < 0)
                    throw new InvalidOperationException("The x position should be at least 0.");

                _x = value;
            }
        }

        /// <summary>
        /// Gets or sets the y position.
        /// </summary>
        [JsonProperty("y")]
        public int Y
        {
            get
            {
                return _y;
            }

            set
            {
                if (value < 0)
                    throw new InvalidOperationException("The y position should be at least 0.");

                _y = value;
            }
        }
    }
}
