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
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Line
{
    internal static class StreamExtensions
    {
        private static readonly byte[] Utf8Preamable = Encoding.UTF8.GetPreamble();

        public static async Task<byte[]> ToArrayAsync(this Stream self)
        {
            if (self.Length == 0)
                return null;

            byte[] content = new byte[self.Length];

            if (content.Length < Utf8Preamable.Length)
            {
                await self.ReadAsync(content, 0, content.Length);
                return content;
            }

            int offset = 0;
            int remaining = content.Length;

            int length = await self.ReadAsync(content, 0, Utf8Preamable.Length);
            remaining -= length;

            /* Ignore the UTF8 Preamable */
            if (IsUtf8Preamable(content))
                Array.Resize(ref content, content.Length - Utf8Preamable.Length);
            else
                offset += length;

            while (remaining > 0)
            {
                length = await self.ReadAsync(content, offset, Math.Min(remaining, 8192));

                remaining -= length;
                offset += length;
            }

            return content;
        }

        private static bool IsUtf8Preamable(byte[] content)
        {
            if (content.Length < Utf8Preamable.Length)
                return false;

            for (int i = 0; i < Utf8Preamable.Length; i++)
            {
                if (content[i] != Utf8Preamable[i])
                    return false;
            }

            return true;
        }
    }
}
