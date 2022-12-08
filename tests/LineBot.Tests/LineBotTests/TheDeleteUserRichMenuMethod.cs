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
        public class TheDeleteUserRichMenuMethod
        {
            public class WithUser
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenUserIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
                    {
                        await bot.DeleteUserRichMenu((IUser)null);
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
                        await bot.DeleteUserRichMenu(user);
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
                        await bot.DeleteUserRichMenu(user);
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
                        await bot.DeleteUserRichMenu(user);
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.DeleteUserRichMenu(user.Id);

                    Assert.Equal(HttpMethod.Delete, httpClient.RequestMethod);
                    Assert.Equal($"/user/{user.Id}/richmenu", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.DeleteUserRichMenu(user);
                    Assert.Same(bot, result);
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
                        await bot.DeleteUserRichMenu((string)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
                    {
                        await bot.DeleteUserRichMenu(string.Empty);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenResponseIsError()
                {
                    var httpClient = TestHttpClient.ThatReturnsAnError();

                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsUnknownError(async () =>
                    {
                        await bot.DeleteUserRichMenu("test");
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var userId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.DeleteUserRichMenu(userId);

                    Assert.Equal(HttpMethod.Delete, httpClient.RequestMethod);
                    Assert.Equal($"/user/{userId}/richmenu", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.DeleteUserRichMenu("test");
                    Assert.Same(bot, result);
                }
            }
        }
    }
}
