// <copyright file="UserProfileTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests.Profile
{
    [TestClass]
    public class UserProfileTests
    {
        private const string UserProfileJson = "UserProfile\\UserProfile.json";

        [TestMethod]
        public async Task GetProfile_UserIdIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
            {
                IUserProfile profile = await bot.GetProfile(null);
            });
        }

        [TestMethod]
        public async Task GetProfile_UserIdIsEmpty_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
            {
                IUserProfile profile = await bot.GetProfile(string.Empty);
            });
        }

        [TestMethod]
        [DeploymentItem(UserProfileJson)]
        public async Task GetProfile_CorrectResponse_ReturnsUserProfile()
        {
            TestHttpClient httpClient = TestHttpClient.Create(UserProfileJson);

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            IUserProfile profile = await bot.GetProfile("test");

            Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
            Assert.AreEqual("profile/test", httpClient.RequestPath);

            Assert.IsNotNull(profile);
            Assert.AreEqual("LINE taro", profile.DisplayName);
            Assert.AreEqual(new Uri("http://obs.line-apps.com/..."), profile.PictureUrl);
            Assert.AreEqual("Hello, LINE!", profile.StatusMessage);
            Assert.AreEqual("Uxxxxxxxxxxxxxx...", profile.UserId);
        }
    }
}
