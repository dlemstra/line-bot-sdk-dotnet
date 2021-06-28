// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates an imagemap area.
    /// </summary>
    public class ImagemapArea : ImagemapSize
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
