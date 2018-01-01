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

namespace Line
{
    internal static class IConfirmTemplateExtensions
    {
        public static ConfirmTemplate ToConfirmTemplate(this IConfirmTemplate self)
        {
            if (self.Text == null)
                throw new InvalidOperationException("The text cannot be null.");

            ConfirmTemplate confirmTemplate = self as ConfirmTemplate;
            if (confirmTemplate == null)
            {
                confirmTemplate = new ConfirmTemplate()
                {
                    Text = self.Text
                };
            }

            if (self.OkAction == null)
                throw new InvalidOperationException("The ok action cannot be null.");

            if (self.CancelAction == null)
                throw new InvalidOperationException("The cancel action cannot be null.");

            confirmTemplate.OkAction = self.OkAction.ToTemplateAction();
            confirmTemplate.CancelAction = self.CancelAction.ToTemplateAction();

            return confirmTemplate;
        }
    }
}
