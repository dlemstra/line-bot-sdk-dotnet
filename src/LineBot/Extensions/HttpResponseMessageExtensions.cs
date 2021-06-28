// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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

            if (self.Content is null)
                throw new LineBotException(self.StatusCode);

            var error = await self.Content.DeserializeObject<LineError>().ConfigureAwait(false);

            if (error is null || string.IsNullOrWhiteSpace(error.Message))
                throw new LineBotException(self.StatusCode);

            throw new LineBotException(self.StatusCode, error);
        }
    }
}
