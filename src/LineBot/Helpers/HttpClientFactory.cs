// <copyright file="HttpClientFactory.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Line
{
    internal static class HttpClientFactory
    {
        private static readonly ConcurrentDictionary<string, HttpClient> Clients = new ConcurrentDictionary<string, HttpClient>();

        public static HttpClient Create(ILineConfiguration configuration)
        {
            Guard.NotNull(nameof(configuration), configuration);

            if (string.IsNullOrWhiteSpace(configuration.ChannelAccessToken))
                throw new InvalidOperationException($"The {nameof(configuration.ChannelAccessToken)} should not be null or whitespace.");

            return Clients.GetOrAdd(configuration.ChannelAccessToken, CreateHttpClient);
        }

        private static HttpClient CreateHttpClient(string channelAccessToken)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.line.me/v2/bot/")
            };
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {channelAccessToken}");

            return client;
        }
    }
}
