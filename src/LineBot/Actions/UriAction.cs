// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a uri action.
    /// </summary>
    public sealed class UriAction : IAction
    {
        private string? _label;
        private Uri? _url;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ActionType>))]
        ActionType IAction.Type
            => ActionType.Uri;

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

        /// <summary>
        /// Gets or sets the url opened when the action is performed.
        /// <para>Protocol: HTTP, HTTPS, LINE, TEL.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// </summary>
        [JsonProperty("uri")]
        public Uri? Url
        {
            get => _url;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The url cannot be null.");

                if (!"http".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase) &&
                    !"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase) &&
                    !"line".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase) &&
                    !"tel".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("The url should use the http, https, line or tel scheme.");

                if (value.ToString().Length > 1000)
                    throw new InvalidOperationException("The url cannot be longer than 1000 characters.");

                _url = value;
            }
        }

        void IAction.Validate()
        {
            if (_label is null)
                throw new InvalidOperationException("The label cannot be null.");

            if (_url is null)
                throw new InvalidOperationException("The url cannot be null.");
        }
    }
}
