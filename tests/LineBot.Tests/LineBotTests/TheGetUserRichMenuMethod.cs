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
        public class TheGetUserRichMenuMethod
        {
            public class WithUser
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenUserIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
                    {
                        await bot.GetUserRichMenu((IUser)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsNull()
                {
                    var user = new TestUser()
                    {
                        Id = null
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
                    {
                        await bot.GetUserRichMenu(user);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsEmpty()
                {
                    var user = new TestUser()
                    {
                        Id = string.Empty
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
                    {
                        await bot.GetUserRichMenu(user);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenResponseIsError()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.ThatReturnsAnError();

                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsUnknownError(async () =>
                    {
                        await bot.GetUserRichMenu(user);
                    });
                }

                [Fact]
                public async Task ShouldReturnNullWhenResponseContainsWhitespace()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.GetUserRichMenu(user);

                    Assert.Null(richMenu);
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.GetUserRichMenu(user.Id);

                    Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
                    Assert.Equal($"/user/{user.Id}/richmenu", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldReturnRichMenuIdWhenResponseIsCorrect()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.UserRichMenuResponse);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenuId = await bot.GetUserRichMenu(user);

                    Assert.Equal("110fb567-e204-4131-9669-db828ce65d2f", richMenuId);
                }
            }

            public class WithUserId
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
                    {
                        await bot.GetUserRichMenu((string)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
                    {
                        await bot.GetUserRichMenu(string.Empty);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenResponseIsError()
                {
                    var httpClient = TestHttpClient.ThatReturnsAnError();

                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsUnknownError(async () =>
                    {
                        await bot.GetUserRichMenu("test");
                    });
                }

                [Fact]
                public async Task ShouldReturnNullWhenResponseContainsWhitespace()
                {
                    var httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.GetUserRichMenu("test");

                    Assert.Null(richMenu);
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var userId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.GetUserRichMenu(userId);

                    Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
                    Assert.Equal($"/user/{userId}/richmenu", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldReturnRichMenuIdWhenResponseIsCorrect()
                {
                    var httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.UserRichMenuResponse);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenuId = await bot.GetUserRichMenu("test");

                    Assert.Equal("110fb567-e204-4131-9669-db828ce65d2f", richMenuId);
                }
            }
        }
    }
}
