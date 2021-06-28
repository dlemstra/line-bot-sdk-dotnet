// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a base class for imagemap actions.
    /// </summary>
    public abstract class ImagemapAction
    {
        private ImagemapArea? _area;
        private string? _label;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ImagemapActionType>))]
        private ImagemapActionType _type;

        internal ImagemapAction(ImagemapActionType type)
        {
            _type = type;
        }

        /// <summary>
        /// Gets or sets the tappable area.
        /// </summary>
        [JsonProperty("area")]
        public ImagemapArea? Area
        {
            get
            {
                return _area;
            }

            set
            {
                if (value is null)
                    throw new InvalidOperationException("The area cannot be null.");

                _area = value;
            }
        }

        /// <summary>
        /// Gets or sets the label.
        /// <para>Max: 50 characters.</para>
        /// </summary>
        [JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
        public string? Label
        {
            get
            {
                return _label;
            }

            set
            {
                if (value?.Length > 50)
                    throw new InvalidOperationException("The label cannot be longer than 50 characters.");

                _label = value;
            }
        }

        internal virtual void Validate()
        {
            if (_area is null)
                throw new InvalidOperationException("The area cannot be null.");
        }
    }
}
