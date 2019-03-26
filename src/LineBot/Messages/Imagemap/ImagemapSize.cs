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
    /// Encapsulates an imagemap size.
    /// </summary>
    public class ImagemapSize
    {
        private int _width;
        private int _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapSize"/> class.
        /// </summary>
        public ImagemapSize()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapSize"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ImagemapSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [JsonProperty("width")]
        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                if (value < 1)
                    throw new InvalidOperationException("The width should be at least 1.");

                _width = value;
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [JsonProperty("height")]
        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                if (value < 1)
                    throw new InvalidOperationException("The height should be at least 1.");

                _height = value;
            }
        }

        internal virtual void Validate()
        {
            if (_width == 0)
                throw new InvalidOperationException("The width should be at least 1.");

            if (_height == 0)
                throw new InvalidOperationException("The height should be at least 1.");
        }
    }
}
