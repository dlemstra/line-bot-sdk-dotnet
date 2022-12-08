// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
                await self.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                return buffer;
            }

            var offset = 0;
            var remaining = buffer.Length;

            var length = await self.ReadAsync(buffer, 0, Utf8Preamable.Length).ConfigureAwait(false);
            remaining -= length;

            /* Ignore the UTF8 Preamable */
            if (IsUtf8Preamable(buffer))
                Array.Resize(ref buffer, buffer.Length - Utf8Preamable.Length);
            else
                offset += length;

            while (remaining > 0)
            {
                length = await self.ReadAsync(buffer, offset, Math.Min(remaining, 8192)).ConfigureAwait(false);

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
                var length = await self.ReadAsync(buffer, 0, Utf8Preamable.Length).ConfigureAwait(false);
                if (length == 0)
                    return null;

                /* Ignore the UTF8 Preamable */
                if (!IsUtf8Preamable(buffer))
                    await memStream.WriteAsync(buffer, 0, length).ConfigureAwait(false);

                while ((length = await self.ReadAsync(buffer, 0, BufferSize).ConfigureAwait(false)) != 0)
                {
                    memStream.Write(buffer, 0, length);
                }

                return memStream.ToArray();
            }
        }

        private static bool IsUtf8Preamable(byte[] content)
        {
            for (var i = 0; i < Utf8Preamable.Length; i++)
            {
                if (content[i] != Utf8Preamable[i])
                    return false;
            }

            return true;
        }
    }
}
