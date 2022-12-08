// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LoggingDelegatingHandlerTests
    {
        public class TheSendAsyncMethod
        {
            [Fact]
            public async Task ShouldLogApiCall()
            {
                var logger = new TestLogger();
                var innerHandler = new TestHttpMessageHandler(new byte[] { });

                var loggingDelegatingHandler = new LoggingDelegatingHandler(logger, innerHandler);

                var httpClient = new HttpClient(loggingDelegatingHandler);

                await httpClient.PostAsync("https://foo.bar", new StringContent("Test"));

                Assert.Equal(new Uri("https://foo.bar/"), logger.LogApiCallUri);

                var content = await logger.LogApiCallHttpContent.ReadAsStringAsync();
                Assert.Equal("Test", content);
            }
        }
    }
}
