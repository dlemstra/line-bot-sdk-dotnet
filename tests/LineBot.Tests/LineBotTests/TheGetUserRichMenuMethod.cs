// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheGetUserRichMenuMethod
        {
            [TestClass]
            public class WithUser
            {
                [TestMethod]
                public async Task ShouldThrowExceptionWhenUserIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
                    {
                        await bot.GetUserRichMenu((IUser)null);
                    });
                }

                [TestMethod]
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

                [TestMethod]
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

                [TestMethod]
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

                [TestMethod]
                [DeploymentItem(JsonDocuments.Whitespace)]
                public async Task ShouldReturnNullWhenResponseContainsWhitespace()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.GetUserRichMenu(user);

                    Assert.IsNull(richMenu);
                }

                [TestMethod]
                [DeploymentItem(JsonDocuments.EmptyObject)]
                public async Task ShouldCallTheCorrectApi()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.GetUserRichMenu(user.Id);

                    Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
                    Assert.AreEqual($"/user/{user.Id}/richmenu", httpClient.RequestPath);
                }

                [TestMethod]
                [DeploymentItem(JsonDocuments.RichMenu.UserRichMenuResponse)]
                public async Task ShouldReturnRichMenuIdWhenResponseIsCorrect()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.UserRichMenuResponse);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenuId = await bot.GetUserRichMenu(user);

                    Assert.AreEqual("110fb567-e204-4131-9669-db828ce65d2f", richMenuId);
                }
            }

            [TestClass]
            public class WithUserId
            {
                [TestMethod]
                public async Task ShouldThrowExceptionWhenUserIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
                    {
                        await bot.GetUserRichMenu((string)null);
                    });
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenUserIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
                    {
                        await bot.GetUserRichMenu(string.Empty);
                    });
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenResponseIsError()
                {
                    var httpClient = TestHttpClient.ThatReturnsAnError();

                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsUnknownError(async () =>
                    {
                        await bot.GetUserRichMenu("test");
                    });
                }

                [TestMethod]
                [DeploymentItem(JsonDocuments.Whitespace)]
                public async Task ShouldReturnNullWhenResponseContainsWhitespace()
                {
                    var httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.GetUserRichMenu("test");

                    Assert.IsNull(richMenu);
                }

                [TestMethod]
                [DeploymentItem(JsonDocuments.EmptyObject)]
                public async Task ShouldCallTheCorrectApi()
                {
                    var userId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenu = await bot.GetUserRichMenu(userId);

                    Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
                    Assert.AreEqual($"/user/{userId}/richmenu", httpClient.RequestPath);
                }

                [TestMethod]
                [DeploymentItem(JsonDocuments.RichMenu.UserRichMenuResponse)]
                public async Task ShouldReturnRichMenuIdWhenResponseIsCorrect()
                {
                    var httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.UserRichMenuResponse);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    var richMenuId = await bot.GetUserRichMenu("test");

                    Assert.AreEqual("110fb567-e204-4131-9669-db828ce65d2f", richMenuId);
                }
            }
        }
    }
}
