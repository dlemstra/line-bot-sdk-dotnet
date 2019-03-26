// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Line
{
    internal static class StringExtensions
    {
        public static void ColorValidate(this string value)
        {
            if (value == null || value.Length != 7)
                throw new InvalidOperationException("The color should be 7 characters long.");

            if (value[0] != '#')
                throw new InvalidOperationException("The color should start with #.");

            if (!IsValidColor(value))
                throw new InvalidOperationException("The color contains invalid characters.");
        }

        private static bool IsValidColor(string value)
        {
            for (int i = 1; i < value.Length; i++)
            {
                char character = value[i];

                // 0-9
                if (character >= 48 && character <= 57)
                    continue;

                // A-F
                if (character >= 65 && character <= 70)
                    continue;

                // a-f
                if (character >= 97 && character <= 102)
                    continue;

                return false;
            }

            return true;
        }
    }
}
