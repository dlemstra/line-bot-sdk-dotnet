// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Xunit;

namespace Line.Tests
{
    public partial class HttpClientFactoryTests
    {
        public class TheCreateMethod
        {
            [Fact]
            public void ShouldReturnNewInstance()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnNewInstance)
                };

                var clientA = HttpClientFactory.Create(configuration, null);
                var clientB = HttpClientFactory.Create(configuration, null);

                Assert.NotEqual(clientA, clientB);
            }

            [Fact]
            public void ShouldReturnInitializedHttpClient()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnInitializedHttpClient)
                };

                var client = HttpClientFactory.Create(configuration, null);

                Assert.Equal(new Uri("https://api.line.me/v2/bot/"), client.BaseAddress);
                var authorization = client.DefaultRequestHeaders.GetValues("Authorization").First();
                Assert.Equal("Bearer " + nameof(ShouldReturnInitializedHttpClient), authorization);
            }

            [Fact]
            public void ShouldSetInnerHandlerToLineBotLogger()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnInitializedHttpClient)
                };

                var client = HttpClientFactory.Create(configuration, null);

                var field = client.GetType().BaseType.GetRuntimeFields().Where(f => f.Name == "_handler").First();
                var logger = field.GetValue(client) as LoggingDelegatingHandler;
                Assert.NotNull(logger);
                Assert.IsType<HttpClientHandler>(logger.InnerHandler);
            }
        }
    }
}
