﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Object describing the boundaries of the area in pixels.
    /// </summary>
    public class RichMenuBounds
    {
        private int _x;
        private int _y;
        private int _width;
        private int _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="RichMenuBounds"/> class.
        /// </summary>
        public RichMenuBounds()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RichMenuBounds"/> class.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RichMenuBounds(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

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

        internal void Validate()
        {
            if (_width == 0)
                throw new InvalidOperationException("The width is not set.");

            if (_height == 0)
                throw new InvalidOperationException("The height is not set.");
        }
    }
}
