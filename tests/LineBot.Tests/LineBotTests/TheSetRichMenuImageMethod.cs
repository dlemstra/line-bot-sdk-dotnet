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
        public class TheSetRichMenuImageMethod
        {
            public class WithRichMenuId
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetRichMenuImage((string)null, null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetRichMenuImage(string.Empty, null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenImageDataIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("imageData", async () =>
                    {
                        await bot.SetRichMenuImage("test", null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenImageDataIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("imageData", async () =>
                    {
                        await bot.SetRichMenuImage("test", new byte[] { });
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var richMenuId = Guid.NewGuid().ToString();
                    var data = new byte[] { 1, 2, 3, 4 };

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.SetRichMenuImage(richMenuId, data);

                    Assert.Equal(HttpMethod.Post, httpClient.RequestMethod);
                    Assert.Equal($"/richmenu/{richMenuId}/content", httpClient.RequestPath);
                    Assert.Equal("01-02-03-04", httpClient.PostedData);
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.SetRichMenuImage(richMenuId, new byte[1] { 0 });
                    });
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.SetRichMenuImage("test", new byte[1] { 0 });
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
                        await bot.SetRichMenuImage((IRichMenuResponse)null, null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetRichMenuImage(new RichMenuResponse(), null);
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
                        await bot.SetRichMenuImage(richMenuResponse, null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenImageDataIsNull()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("imageData", async () =>
                    {
                        await bot.SetRichMenuImage(richMenuResponse, null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenImageDataIsEmpty()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("imageData", async () =>
                    {
                        await bot.SetRichMenuImage(richMenuResponse, new byte[] { });
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

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.SetRichMenuImage(richMenuResponse, data);

                    Assert.Equal(HttpMethod.Post, httpClient.RequestMethod);
                    Assert.Equal($"/richmenu/{richMenuId}/content", httpClient.RequestPath);
                    Assert.Equal("01-02-03-04", httpClient.PostedData);
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
                        await bot.SetRichMenuImage(richMenuResponse, new byte[1] { 0 });
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

                    var result = await bot.SetRichMenuImage(richMenuResponse, new byte[1] { 0 });
                    Assert.Same(bot, result);
                }
            }
        }
    }
}
