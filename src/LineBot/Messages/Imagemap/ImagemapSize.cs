// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
            get => _width;
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
            get => _height;
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
