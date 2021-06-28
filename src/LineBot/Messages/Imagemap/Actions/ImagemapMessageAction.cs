// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates an imagemap message action.
    /// </summary>
    public sealed class ImagemapMessageAction : ImagemapAction
    {
        private string? _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapMessageAction"/> class.
        /// </summary>
        public ImagemapMessageAction()
            : base(ImagemapActionType.Message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapMessageAction"/> class.
        /// </summary>
        /// <param name="text">
        /// The text of the message.
        /// <para>Max: 400 characters.</para>
        /// </param>
        /// <param name="area">The tappable area.</param>
        public ImagemapMessageAction(string text, ImagemapArea area)
            : this()
        {
            Text = text;
            Area = area;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapMessageAction"/> class.
        /// </summary>
        /// <param name="text">
        /// The text of the message.
        /// <para>Max: 400 characters.</para>
        /// </param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ImagemapMessageAction(string text, int x, int y, int width, int height)
            : this(text, new ImagemapArea(x, y, width, height))
        {
        }

        /// <summary>
        /// Gets or sets the text of the message.
        /// <para>Max: 400 characters.</para>
        /// </summary>
        [JsonProperty("text")]
        public string? Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The text cannot be null or whitespace.");

                if (value.Length > 400)
                    throw new InvalidOperationException("The text cannot be longer than 400 characters.");

                _text = value;
            }
        }

        internal override void Validate()
        {
            base.Validate();

            if (_text is null)
                throw new InvalidOperationException("The text cannot be null.");
        }
    }
}
