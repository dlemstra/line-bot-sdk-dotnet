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

namespace Line
{
    internal static class IActionExtensions
    {
        public static void CheckActionType(this IAction self)
        {
            if (self is PostbackAction)
                return;

            if (self is MessageAction)
                return;

            if (self is UriAction)
                return;

            if (self is CameraAction)
                return;

            if (self is DateTimePickerAction)
                return;

            throw new NotSupportedException("The action type is invalid.");
        }

        public static void Validate(this IEnumerable<IAction> self)
        {
            foreach (var action in self)
            {
                action.Validate();
            }
        }
    }
}
