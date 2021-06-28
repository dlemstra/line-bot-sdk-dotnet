// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Rich menu object.
    /// </summary>
    public class RichMenu
    {
        private RichMenuArea[]? _areas;
        private string? _chatBarText;
        private string? _name;
        private RichMenuSize? _size;

        /// <summary>
        /// Gets or sets the array of area objects which define the coordinates and size of tappable areas.
        /// Max: 20 area objects.
        /// </summary>
        [JsonProperty("areas")]
        public RichMenuArea[]? Areas
        {
            get => _areas;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The areas cannot be null.");

                if (value.Length < 1)
                    throw new InvalidOperationException("The minimum number of areas is 1.");

                if (value.Length > 20)
                    throw new InvalidOperationException("The maximum number of areas is 20.");

                _areas = value;
            }
        }

        /// <summary>
        /// Gets or sets the text displayed in the chat bar.
        /// Max: 14 characters.
        /// </summary>
        [JsonProperty("chatBarText")]
        public string? ChatBarText
        {
            get => _chatBarText;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The chat bar text cannot be null or whitespace.");

                if (value.Length > 14)
                    throw new InvalidOperationException("The chat bar text cannot be longer than 14 characters.");

                _chatBarText = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the rich menu. This value can be used to help manage your rich menus and is not displayed to users.
        /// Max: 300 characters.
        /// </summary>
        [JsonProperty("name")]
        public string? Name
        {
            get => _name;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The name cannot be null or whitespace.");

                if (value.Length > 300)
                    throw new InvalidOperationException("The name cannot be longer than 300 characters.");

                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the rich menu should be displayed by default.
        /// </summary>
        [JsonProperty("selected")]
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the object which contains the width and height of the rich menu displayed in the chat.
        /// Rich menu images must be one of the following sizes: 2500x1686px or 2500x843px.
        /// </summary>
        [JsonProperty("size")]
        public RichMenuSize? Size
        {
            get => _size;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The size cannot be null.");

                _size = value;
            }
        }

        internal void Validate()
        {
            if (_areas is null)
                throw new InvalidOperationException("The areas cannot be null.");

            if (_chatBarText is null)
                throw new InvalidOperationException("The chat bar text cannot be null.");

            if (_name is null)
                throw new InvalidOperationException("The name cannot be null.");

            if (_size is null)
                throw new InvalidOperationException("The size cannot be null.");

            _areas.Validate();
            _size.Validate();
        }
    }
}
