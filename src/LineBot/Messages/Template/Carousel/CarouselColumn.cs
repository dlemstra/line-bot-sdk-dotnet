// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a carousel column.
    /// </summary>
    public sealed class CarouselColumn
    {
        private Uri? _thumbnailUrl;
        private string? _title;
        private string? _text;
        private string? _color;
        private IAction? _defaultAction;
        private IEnumerable<IAction>? _actions;

        /// <summary>
        /// Gets or sets the image url for the thumbnail.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG or PNG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Aspect ratio: 1:1.51.</para>
        /// <para>Max width: 1024px.</para>
        /// <para>Max size: 1 MB.</para>
        /// </summary>
        [JsonProperty("thumbnailImageUrl")]
        public Uri? ThumbnailUrl
        {
            get
            {
                return _thumbnailUrl;
            }

            set
            {
                if (value is not null)
                {
                    if (!"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("The thumbnail url should use the https scheme.");

                    if (value.ToString().Length > 1000)
                        throw new InvalidOperationException("The thumbnail url cannot be longer than 1000 characters.");
                }

                _thumbnailUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets background color of image. Specify a RGB color value. The default value is #FFFFFF (white).
        /// </summary>
        [JsonProperty("imageBackgroundColor")]
        public string? ImageBackgroundColor
        {
            get
            {
                return _color;
            }

            set
            {
                ColorHelper.Validate(value);

                _color = value.ToUpperInvariant();
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// <para>Max: 40 characters.</para>
        /// </summary>
        [JsonProperty("title")]
        public string? Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (value is not null && value.Length > 40)
                    throw new InvalidOperationException("The title cannot be longer than 40 characters.");

                _title = value;
            }
        }

        /// <summary>
        /// Gets or sets the message text.
        /// <para>Max: 120 characters (no image or title).</para>
        /// <para>Max: 60 characters (message with an image or title).</para>
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

                if (value.Length > 120)
                    throw new InvalidOperationException("The text cannot be longer than 120 characters.");

                if ((ThumbnailUrl is not null || Title is not null) && value.Length > 60)
                    throw new InvalidOperationException("The text cannot be longer than 60 characters when the thumbnail url or title are set.");

                _text = value;
            }
        }

        /// <summary>
        /// Gets or sets the action when image is tapped; set for the entire image, title, and text area.
        /// </summary>
        [JsonProperty("defaultAction")]
        public IAction? DefaultAction
        {
            get
            {
                return _defaultAction;
            }

            set
            {
                if (value is not null)
                    value.CheckActionType();

                _defaultAction = value;
            }
        }

        /// <summary>
        /// Gets or sets the actions when tapped.
        /// <para>Max: 4.</para>
        /// </summary>
        [JsonProperty("actions")]
        public IEnumerable<IAction>? Actions
        {
            get
            {
                return _actions;
            }

            set
            {
                if (value is null)
                    throw new InvalidOperationException("The actions cannot be null.");

                int count = value.Count();

                if (count < 1)
                    throw new InvalidOperationException("The minimum number of actions is 1.");

                if (count > 3)
                    throw new InvalidOperationException("The maximum number of actions is 3.");

                foreach (var action in value)
                {
                    action.CheckActionType();
                }

                _actions = value;
            }
        }

        internal void Validate()
        {
            if (_text is null)
                throw new InvalidOperationException("The text cannot be null.");

            if (_actions is null)
                throw new InvalidOperationException("The actions cannot be null.");

            _actions.Validate();
        }
    }
}
