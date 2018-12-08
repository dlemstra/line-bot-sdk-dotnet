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
        private Uri _thumbnailUrl;
        private string _title;
        private string _text;
        private string _color = "#FFFFFF";
        private IAction _defaultAction;
        private IEnumerable<IAction> _actions;

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
        public Uri ThumbnailUrl
        {
            get
            {
                return _thumbnailUrl;
            }

            set
            {
                if (value != null)
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
        public string ImageBackgroundColor
        {
            get
            {
                return _color;
            }

            set
            {
                if (value == null || value.Length != 7)
                    throw new InvalidOperationException("The color should be 7 characters long.");

                if (value[0] != '#')
                    throw new InvalidOperationException("The color should start with #.");

                if (!IsValidColor(value))
                    throw new InvalidOperationException("The color contains invalid characters.");

                _color = value.ToUpperInvariant();
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// <para>Max: 40 characters.</para>
        /// </summary>
        [JsonProperty("title")]
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (value != null && value.Length > 40)
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
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The text cannot be null or whitespace.");

                if (value.Length > 120)
                    throw new InvalidOperationException("The text cannot be longer than 120 characters.");

                if ((ThumbnailUrl != null || Title != null) && value.Length > 60)
                    throw new InvalidOperationException("The text cannot be longer than 60 characters when the thumbnail url or title are set.");

                _text = value;
            }
        }

        /// <summary>
        /// Gets or sets the action when image is tapped; set for the entire image, title, and text area.
        /// </summary>
        [JsonProperty("defaultAction")]
        public IAction DefaultAction
        {
            get
            {
                return _defaultAction;
            }

            set
            {
                if (value != null)
                    value.CheckActionType();

                _defaultAction = value;
            }
        }

        /// <summary>
        /// Gets or sets the actions when tapped.
        /// <para>Max: 4.</para>
        /// </summary>
        [JsonProperty("actions")]
        public IEnumerable<IAction> Actions
        {
            get
            {
                return _actions;
            }

            set
            {
                if (value == null)
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
            if (_text == null)
                throw new InvalidOperationException("The text cannot be null.");

            if (_actions == null)
                throw new InvalidOperationException("The actions cannot be null.");

            _actions.Validate();
        }

        private static bool IsValidColor(string value)
        {
            for (int i = 1; i < value.Length; i++)
            {
                char character = value[i];

                // 0-9
                if (character >= 48 && character <= 57)
                    continue;

                // A-F
                if (character >= 65 && character <= 70)
                    continue;

                // a-f
                if (character >= 97 && character <= 102)
                    continue;

                return false;
            }

            return true;
        }
    }
}
