// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a confirm template.
    /// </summary>
    public sealed class ConfirmTemplate : ITemplate
    {
        private string? _text;
        private IAction? _okAction;
        private IAction? _cancelAction;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<TemplateType>))]
        TemplateType ITemplate.Type
            => TemplateType.Confirm;

        /// <summary>
        /// Gets or sets the message text.
        /// <para>Max: 240 characters (no image or title).</para>
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

                if (value.Length > 240)
                    throw new InvalidOperationException("The text cannot be longer than 240 characters.");

                _text = value;
            }
        }

        /// <summary>
        /// Gets or sets the action for the OK button.
        /// </summary>
        [JsonIgnore]
        public IAction? OkAction
        {
            get
            {
                return _okAction;
            }

            set
            {
                if (value is null)
                    throw new InvalidOperationException("The ok action cannot be null.");

                value.CheckActionType();

                _okAction = value;
            }
        }

        /// <summary>
        /// Gets or sets the action for the Cancel button.
        /// </summary>
        [JsonIgnore]
        public IAction? CancelAction
        {
            get
            {
                return _cancelAction;
            }

            set
            {
                if (value is null)
                    throw new InvalidOperationException("The cancel action cannot be null.");

                value.CheckActionType();

                _cancelAction = value;
            }
        }

        [JsonProperty("actions")]
        private IAction[] Actions
            => new IAction[] { _okAction!, _cancelAction! };

        void ITemplate.Validate()
        {
            if (_text is null)
                throw new InvalidOperationException("The text cannot be null.");

            if (_okAction is null)
                throw new InvalidOperationException("The ok action cannot be null.");

            if (_cancelAction is null)
                throw new InvalidOperationException("The cancel action cannot be null.");

            _okAction.Validate();
            _cancelAction.Validate();
        }
    }
}
