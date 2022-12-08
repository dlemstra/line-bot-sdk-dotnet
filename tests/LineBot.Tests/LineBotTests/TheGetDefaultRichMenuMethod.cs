// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheGetDefaultRichMenuMethod
        {
            [Fact]
            public async Task ReturnsIdWhenResponseIsCorrect()
            {
                var httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.DefaultRichMenuId);
                var bot = TestConfiguration.CreateBot(httpClient);
                var id = await bot.GetDefaultRichMenu();

                Assert.Equal("110FB567-E204-4131-9669-DB828CE65D2F", id);
            }

            [Fact]
            public async Task ReturnsNullIdWhenResponseContainsEmptyObject()
            {
                var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                var bot = TestConfiguration.CreateBot(httpClient);
                var id = await bot.GetDefaultRichMenu();

                Assert.Null(id);
            }

            [Fact]
            public async Task ReturnsNullIdWhenResponseContainsWhitespace()
            {
                var httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                var bot = TestConfiguration.CreateBot(httpClient);
                var id = await bot.GetDefaultRichMenu();

                Assert.Null(id);
            }

            [Fact]
            public async Task ThrowsExceptionWhenResponseIsError()
            {
                var httpClient = TestHttpClient.ThatReturnsAnError();
                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsAsync<LineBotException>("Unknown error", async () =>
                {
                    await bot.GetDefaultRichMenu();
                });
            }
        }
    }
}
