// <copyright file="HttpContentExtensions.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Line
{
    internal static class HttpContentExtensions
    {
        public static async Task<T> DeserializeObject<T>(this HttpContent self)
        {
            string body = await self.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
