// <copyright file="HttpResponseMessageExtensions.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Net.Http;
using System.Threading.Tasks;

namespace Line
{
    internal static class HttpResponseMessageExtensions
    {
        public static async Task CheckResult(this HttpResponseMessage self)
        {
            if (self.IsSuccessStatusCode)
                return;

            if (self.Content == null)
                throw new LineBotException(self.StatusCode);

            LineError error = await self.Content.DeserializeObject<LineError>();

            if (error == null || string.IsNullOrWhiteSpace(error.Message))
                throw new LineBotException(self.StatusCode);

            throw new LineBotException(self.StatusCode, error);
        }
    }
}
