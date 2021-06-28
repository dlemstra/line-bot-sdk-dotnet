// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Net.Http;

namespace Line.Tests
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetPostedData(this HttpRequestMessage self)
        {
            return self.Headers.GetValues(nameof(HttpRequestMessageExtensions)).First();
        }

        public static void SetPostedData(this HttpRequestMessage self)
        {
            if (self.Content is StringContent stringContent)
            {
                var data = stringContent.ReadAsStringAsync().GetAwaiter().GetResult();
                self.Headers.Add(nameof(HttpRequestMessageExtensions), data);
            }

            if (self.Content is ByteArrayContent byteArrayContent)
            {
                var data = byteArrayContent.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                self.Headers.Add(nameof(HttpRequestMessageExtensions), BitConverter.ToString(data));
            }
        }
    }
}
