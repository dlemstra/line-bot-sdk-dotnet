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
        public class TheGetRichMenuImageMethod
        {
            public class WithRichMenuId
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.GetRichMenuImage((string)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.GetRichMenuImage(string.Empty);
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var richMenuId = Guid.NewGuid().ToString();
                    var data = new byte[] { 1, 2, 3, 4 };

                    var httpClient = TestHttpClient.ThatReturnsData(data);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var result = await bot.GetRichMenuImage(richMenuId);

                    Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
                    Assert.Equal($"/richmenu/{richMenuId}/content", httpClient.RequestPath);
                    Assert.Equal(data, result);
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.GetRichMenuImage(richMenuId);
                    });
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
                        await bot.GetRichMenuImage((IRichMenuResponse)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.GetRichMenuImage(new RichMenuResponse());
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
                        await bot.GetRichMenuImage(richMenuResponse);
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
                    var data = new byte[] { 1, 2, 3, 4 };

                    var httpClient = TestHttpClient.ThatReturnsData(data);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var result = await bot.GetRichMenuImage(richMenuResponse);

                    Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
                    Assert.Equal($"/richmenu/{richMenuId}/content", httpClient.RequestPath);
                    Assert.Equal(data, result);
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
                        await bot.GetRichMenuImage(richMenuResponse);
                    });
                }
            }
        }
    }
}
