// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

namespace Line
{
    internal sealed class Guard
    {
        public static void NotNull(string paramName, object value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException(paramName);
        }

        public static void NotNullOrEmpty(string paramName, string value)
        {
            NotNull(paramName, value);

            if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.", paramName);
        }

        public static void NotNullOrEmpty<T>(string paramName, T[] value)
        {
            NotNull(paramName, value);

            if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.", paramName);
        }

        public static void NotNullOrEmpty<T>(string paramName, IEnumerable<T> value)
        {
            NotNull(paramName, value);

            if (!value.Any())
                throw new ArgumentException("Value cannot be empty.", paramName);
        }
    }
}
