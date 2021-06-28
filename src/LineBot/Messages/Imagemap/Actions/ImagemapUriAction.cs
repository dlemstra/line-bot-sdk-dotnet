// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates an imagemap uri action.
    /// </summary>
    public sealed class ImagemapUriAction : ImagemapAction
    {
        private Uri? _url;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapUriAction"/> class.
        /// </summary>
        public ImagemapUriAction()
            : base(ImagemapActionType.Uri)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapUriAction"/> class.
        /// </summary>
        /// <param name="url">
        /// The webpage url.
        /// <para>Max url length: 1000 characters.</para>
        /// </param>
        /// <param name="area">The tappable area.</param>
        public ImagemapUriAction(string url, ImagemapArea area)
            : this(new Uri(url), area)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapUriAction"/> class.
        /// </summary>
        /// <param name="url">
        /// The webpage url.
        /// <para>Max url length: 1000 characters.</para>
        /// </param>
        /// <param name="area">The tappable area.</param>
        public ImagemapUriAction(Uri url, ImagemapArea area)
            : this()
        {
            Url = url;
            Area = area;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapUriAction"/> class.
        /// </summary>
        /// <param name="url">
        /// The webpage url.
        /// <para>Max url length: 1000 characters.</para>
        /// </param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ImagemapUriAction(string url, int x, int y, int width, int height)
            : this(new Uri(url), x, y, width, height)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagemapUriAction"/> class.
        /// </summary>
        /// <param name="url">
        /// The webpage url.
        /// <para>Max url length: 1000 characters.</para>
        /// </param>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ImagemapUriAction(Uri url, int x, int y, int width, int height)
            : this(url, new ImagemapArea(x, y, width, height))
        {
        }

        /// <summary>
        /// Gets or sets the webpage url.
        /// <para>Max url length: 1000 characters.</para>
        /// </summary>
        [JsonProperty("linkUri")]
        public Uri? Url
        {
            get => _url;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The url cannot be null.");

                if (value.ToString().Length > 1000)
                    throw new InvalidOperationException("The url cannot be longer than 1000 characters.");

                _url = value;
            }
        }

        internal override void Validate()
        {
            base.Validate();

            if (_url is null)
                throw new InvalidOperationException("The url cannot be null.");
        }
    }
}
