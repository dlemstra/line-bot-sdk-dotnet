// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
    /// Encapsulates an imagemap uri action.
    /// </summary>
    public sealed class ImagemapUriAction : ImagemapAction, IImagemapUriAction
    {
        private Uri _url;

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
        public Uri Url
        {
            get
            {
                return _url;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The url cannot be null.");

                if (value.ToString().Length > 1000)
                    throw new InvalidOperationException("The url cannot be longer than 1000 characters.");

                _url = value;
            }
        }

        internal static ImagemapUriAction Convert(IImagemapUriAction action)
        {
            if (action.Area == null)
                throw new InvalidOperationException("The area cannot be null.");

            if (action.Url == null)
                throw new InvalidOperationException("The url cannot be null.");

            if (!(action is ImagemapUriAction imagemapUriAction))
            {
                imagemapUriAction = new ImagemapUriAction()
                {
                    Url = action.Url
                };
            }

            imagemapUriAction.Area = action.Area.ToImagemapArea();

            return imagemapUriAction;
        }
    }
}
