// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a image carousel column.
    /// </summary>
    public sealed class ImageCarouselColumn
    {
        private Uri? _imageUrl;
        private IAction? _action;

        /// <summary>
        /// Gets or sets the image url for the image carousel.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG or PNG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Aspect ratio: 1:1.</para>
        /// <para>Max width: 1024px.</para>
        /// <para>Max size: 1 MB.</para>
        /// </summary>
        [JsonProperty("imageUrl")]
        public Uri? ImageUrl
        {
            get
            {
                return _imageUrl;
            }

            set
            {
                if (value is not null)
                {
                    if (!"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("The image url should use the https scheme.");

                    if (value.ToString().Length > 1000)
                        throw new InvalidOperationException("The image url cannot be longer than 1000 characters.");
                }

                _imageUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the action when image is tapped.
        /// </summary>
        [JsonProperty("action")]
        public IAction? Action
        {
            get
            {
                return _action;
            }

            set
            {
                if (value is null)
                    throw new InvalidOperationException("The action cannot be null.");

                value.CheckActionType();

                _action = value;
            }
        }

        internal void Validate()
        {
            if (_imageUrl is null)
                throw new InvalidOperationException("The image url cannot be null.");

            if (_action is null)
                throw new InvalidOperationException("The action cannot be null.");

            _action.Validate();
        }
    }
}
