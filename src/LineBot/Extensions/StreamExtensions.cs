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
    internal static class StreamExtensions
    {
        private const int BufferSize = 8192;
        private static readonly byte[] Utf8Preamable = Encoding.UTF8.GetPreamble();

        public static Task<byte[]?> ToArrayAsync(this Stream self)
        {
            if (self.CanSeek)
                return ToArrayWithSeekableStream(self);
            else
                return ToArrayWithNonSeekableStream(self);
        }

        private static async Task<byte[]?> ToArrayWithSeekableStream(Stream self)
        {
            if (self.Length == 0)
                return null;

            var buffer = new byte[self.Length];

            if (buffer.Length < Utf8Preamable.Length)
            {
                await self.ReadAsync(buffer, 0, buffer.Length);
                return buffer;
            }

            int offset = 0;
            int remaining = buffer.Length;

            int length = await self.ReadAsync(buffer, 0, Utf8Preamable.Length);
            remaining -= length;

            /* Ignore the UTF8 Preamable */
            if (IsUtf8Preamable(buffer))
                Array.Resize(ref buffer, buffer.Length - Utf8Preamable.Length);
            else
                offset += length;

            while (remaining > 0)
            {
                length = await self.ReadAsync(buffer, offset, Math.Min(remaining, 8192));

                remaining -= length;
                offset += length;
            }

            return buffer;
        }

        private static async Task<byte[]?> ToArrayWithNonSeekableStream(Stream self)
        {
            var buffer = new byte[BufferSize];
            using (var memStream = new MemoryStream())
            {
                int length = await self.ReadAsync(buffer, 0, Utf8Preamable.Length);
                if (length == 0)
                    return null;

                /* Ignore the UTF8 Preamable */
                if (!IsUtf8Preamable(buffer))
                    await memStream.WriteAsync(buffer, 0, length);

                while ((length = await self.ReadAsync(buffer, 0, BufferSize)) != 0)
                {
                    memStream.Write(buffer, 0, length);
                }

                return memStream.ToArray();
            }
        }

        private static bool IsUtf8Preamable(byte[] content)
        {
            for (int i = 0; i < Utf8Preamable.Length; i++)
            {
                if (content[i] != Utf8Preamable[i])
                    return false;
            }

            return true;
        }
    }
}
