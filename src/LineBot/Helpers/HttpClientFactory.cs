// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;

namespace Line
{
    internal static class HttpClientFactory
    {
        public static HttpClient Create(ILineConfiguration configuration, ILineBotLogger logger)
        {
            var loggingDelegatingHandler = new LoggingDelegatingHandler(logger);

            var client = new HttpClient(loggingDelegatingHandler)
            {
                BaseAddress = new Uri("https://api.line.me/v2/bot/")
            };
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration.ChannelAccessToken}");

            return client;
        }
    }
}
