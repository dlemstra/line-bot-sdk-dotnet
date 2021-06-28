// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Define the coordinates and size of tappable area.
    /// </summary>
    public class RichMenuArea
    {
        private IAction? _action;
        private RichMenuBounds? _bounds;

        /// <summary>
        /// Gets or sets the action performed when the area is tapped.
        /// </summary>
        [JsonProperty("action")]
        [JsonConverter(typeof(IActionConverter))]
        public IAction? Action
        {
            get => _action;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The action cannot be null.");

                _action = value;
            }
        }

        /// <summary>
        /// Gets or sets the objects describing the boundaries of the area in pixels.
        /// </summary>
        [JsonProperty("bounds")]
        public RichMenuBounds? Bounds
        {
            get => _bounds;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The bounds cannot be null.");

                _bounds = value;
            }
        }

        internal void Validate()
        {
            if (_action is null)
                throw new InvalidOperationException("The action cannot be null.");

            if (_bounds is null)
                throw new InvalidOperationException("The bounds cannot be null.");

            _action.Validate();
            _bounds.Validate();
        }
    }
}