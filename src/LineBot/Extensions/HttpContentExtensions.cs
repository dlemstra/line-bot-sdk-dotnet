// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Line
{
    internal static class HttpContentExtensions
    {
        public static async Task<T?> DeserializeObject<T>(this HttpContent self)
        {
            var body = await self.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
