// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class UserProfileTests
    {
        [TestMethod]
        public async Task GetProfile_UserIsNulll_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
            {
                IUserProfile profile = await bot.GetProfile((IUser)null);
            });
        }

        [TestMethod]
        public async Task GetProfile_UserIdIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
            {
                IUserProfile profile = await bot.GetProfile((string)null);
            });
        }

        [TestMethod]
        public async Task GetProfile_UserIdIsEmpty_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
            {
                IUserProfile profile = await bot.GetProfile(string.Empty);
            });
        }

        [TestMethod]
        public async Task GetProfile_ErrorResponse_ThrowsException()
        {
            TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.GetProfile("test");
            });
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.UserProfile)]
        public async Task GetProfile_CorrectResponse_ReturnsUserProfile()
        {
            TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.UserProfile);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            IUserProfile profile = await bot.GetProfile("test");

            Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
            Assert.AreEqual("/profile/test", httpClient.RequestPath);

            Assert.IsNotNull(profile);
            Assert.AreEqual("LINE taro", profile.DisplayName);
            Assert.AreEqual(new Uri("http://obs.line-apps.com/..."), profile.PictureUrl);
            Assert.AreEqual("Hello, LINE!", profile.StatusMessage);
            Assert.AreEqual("Uxxxxxxxxxxxxxx...", profile.UserId);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.UserProfile)]
        public async Task GetProfile_WithUser_ReturnsUserProfile()
        {
            TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.UserProfile);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            IUserProfile profile = await bot.GetProfile(new TestUser());

            Assert.AreEqual("/profile/testUser", httpClient.RequestPath);
            Assert.IsNotNull(profile);
        }
    }
}
