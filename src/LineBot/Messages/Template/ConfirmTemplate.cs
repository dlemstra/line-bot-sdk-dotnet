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
    /// Encapsulates a confirm template.
    /// </summary>
    public sealed class ConfirmTemplate : IConfirmTemplate
    {
        private string _text;
        private ITemplateAction _okAction;
        private ITemplateAction _cancelAction;

#pragma warning disable 0414 // Suppress value is never used.

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<TemplateType>))]
        private readonly TemplateType _type = TemplateType.Confirm;

#pragma warning restore 0414

        /// <summary>
        /// Gets or sets the message text.
        /// <para>Max: 240 characters (no image or title).</para>
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

                if (value.Length > 240)
                    throw new InvalidOperationException("The text cannot be longer than 240 characters.");

                _text = value;
            }
        }

        /// <summary>
        /// Gets or sets the action for the OK button.
        /// </summary>
        [JsonIgnore]
        public ITemplateAction OkAction
        {
            get
            {
                return _okAction;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The ok action cannot be null.");

                value.CheckActionType();

                _okAction = value;
            }
        }

        /// <summary>
        /// Gets or sets the action for the Cancel button.
        /// </summary>
        [JsonIgnore]
        public ITemplateAction CancelAction
        {
            get
            {
                return _cancelAction;
            }

            set
            {
                if (value == null)
                    throw new InvalidOperationException("The cancel action cannot be null.");

                value.CheckActionType();

                _cancelAction = value;
            }
        }

        [JsonProperty("actions")]
        private ITemplateAction[] Actions => new ITemplateAction[] { _okAction, _cancelAction };

        internal static ConfirmTemplate Convert(IConfirmTemplate template)
        {
            if (template.Text == null)
                throw new InvalidOperationException("The text cannot be null.");

            if (!(template is ConfirmTemplate confirmTemplate))
            {
                confirmTemplate = new ConfirmTemplate()
                {
                    Text = template.Text
                };
            }

            if (template.OkAction == null)
                throw new InvalidOperationException("The ok action cannot be null.");

            if (template.CancelAction == null)
                throw new InvalidOperationException("The cancel action cannot be null.");

            confirmTemplate.OkAction = TemplateAction.Convert(template.OkAction);
            confirmTemplate.CancelAction = TemplateAction.Convert(template.CancelAction);

            return confirmTemplate;
        }
    }
}
