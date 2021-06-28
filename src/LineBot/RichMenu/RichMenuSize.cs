// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// RichMenuSize object which contains the width and height of the rich menu displayed in the chat.
    /// Rich menu images must be one of the following sizes: 2500x1686px or 2500x843px.
    /// </summary>
    public class RichMenuSize
    {
        private int _height;

        /// <summary>
        /// Initializes a new instance of the <see cref="RichMenuSize"/> class.
        /// </summary>
        public RichMenuSize()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RichMenuSize"/> class.
        /// </summary>
        /// <param name="height">The height of the rich menu. Possible values: 1686, 843.</param>
        public RichMenuSize(int height)
            => Height = height;

        /// <summary>
        /// Gets the width of the rich menu. Will always be 2500.
        /// </summary>
        [JsonProperty("width")]
        public int Width
            => 2500;

        /// <summary>
        /// Gets or sets the height of the rich menu. Possible values: 1686, 843.
        /// </summary>
        [JsonProperty("height")]
        public int Height
        {
            get => _height;
            set
            {
                if (value != 843 && value != 1686)
                    throw new InvalidOperationException("The possible height values are: 1686, 843.");

                _height = value;
            }
        }

        internal void Validate()
        {
            if (_height == 0)
                throw new InvalidOperationException("The height is not set.");
        }
    }
}