// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheDeleteDefaultRichMenuMethod
        {
            [Fact]
            public async Task ShouldCallTheCorrectApi()
            {
                var httpClient = TestHttpClient.Create();
                var bot = TestConfiguration.CreateBot(httpClient);
                await bot.DeleteDefaultRichMenu();

                Assert.Equal(HttpMethod.Delete, httpClient.RequestMethod);
                Assert.Equal($"/user/all/richmenu", httpClient.RequestPath);
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
            {
                var httpClient = TestHttpClient.ThatReturnsAnError();
                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                {
                    await bot.DeleteDefaultRichMenu();
                });
            }

            [Fact]
            public async Task ShouldReturnTheInstance()
            {
                var httpClient = TestHttpClient.Create();
                var bot = TestConfiguration.CreateBot(httpClient);

                var result = await bot.DeleteDefaultRichMenu();
                Assert.Same(bot, result);
            }
        }
    }
}
