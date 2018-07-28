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
using System.Reflection;

namespace Line
{
    internal static class ITemplateActionExtensions
    {
        public static void CheckActionType(this ITemplateAction self)
        {
            var interfaces = self.GetType().GetTypeInfo().ImplementedInterfaces;
            if (!interfaces.Contains(typeof(IPostbackAction)) &&
                !interfaces.Contains(typeof(IMessageAction)) &&
                !interfaces.Contains(typeof(IUriAction)))
                ThrowException();
        }

        public static IEnumerable<ITemplateAction> ToTemplateAction(this IEnumerable<ITemplateAction> self)
        {
            foreach (ITemplateAction action in self)
            {
                yield return action.ToTemplateAction();
            }
        }

        public static ITemplateAction ToTemplateAction(this ITemplateAction self)
        {
            switch (self)
            {
                case IPostbackAction postbackAction:
                    return postbackAction.ToPostbackAction();
                case IMessageAction messageAction:
                    return MessageAction.Convert(messageAction);
                case IUriAction uriAction:
                    return uriAction.ToUriAction();
                default:
                    ThrowException();
                    return null;
            }
        }

        private static void ThrowException()
        {
            throw new NotSupportedException($"The template action type is invalid. Supported types are: {nameof(IPostbackAction)}, {nameof(IMessageAction)} and {nameof(IUriAction)}.");
        }
    }
}
