// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a camera action.
    /// </summary>
    public sealed class CameraAction : IAction
    {
        private string? _label;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ActionType>))]
        ActionType IAction.Type
            => ActionType.Camera;

        /// <summary>
        /// Gets or sets the label.
        /// <para>Max: 20 characters.</para>
        /// </summary>
        [JsonProperty("label")]
        public string? Label
        {
            get => _label;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The label cannot be null or whitespace.");

                if (value.Length > 20)
                    throw new InvalidOperationException("The label cannot be longer than 20 characters.");

                _label = value;
            }
        }

        void IAction.Validate()
        {
            if (_label is null)
                throw new InvalidOperationException("The label cannot be null.");
        }
    }
}
