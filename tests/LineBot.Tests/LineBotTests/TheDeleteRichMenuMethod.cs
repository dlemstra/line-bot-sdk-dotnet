// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheDeleteRichMenuMethod
        {
            public class WithRichMenuId
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.DeleteRichMenu((string)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.DeleteRichMenu(string.Empty);
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.DeleteRichMenu(richMenuId);

                    Assert.Equal(HttpMethod.Delete, httpClient.RequestMethod);
                    Assert.Equal($"/richmenu/{richMenuId}", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.DeleteRichMenu(richMenuId);
                    });
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.DeleteRichMenu("test");
                    Assert.Same(bot, result);
                }
            }

            public class WithRichMenuResponse
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuResponseIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenu", async () =>
                    {
                        await bot.DeleteRichMenu((IRichMenuResponse)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.DeleteRichMenu(new RichMenuResponse());
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = string.Empty
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.DeleteRichMenu(richMenuResponse);
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var richMenuId = Guid.NewGuid().ToString();
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = richMenuId
                    };

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.DeleteRichMenu(richMenuResponse);

                    Assert.Equal(HttpMethod.Delete, httpClient.RequestMethod);
                    Assert.Equal($"/richmenu/{richMenuId}", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.DeleteRichMenu(richMenuResponse);
                    });
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.DeleteRichMenu(richMenuResponse);
                    Assert.Same(bot, result);
                }
            }
        }
    }
}
