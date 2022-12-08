// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class UserProfileTests
    {
        [Fact]
        public async Task GetProfile_UserIsNulll_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
            {
                var profile = await bot.GetProfile((IUser)null);
            });
        }

        [Fact]
        public async Task GetProfile_UserIdIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
            {
                var profile = await bot.GetProfile((string)null);
            });
        }

        [Fact]
        public async Task GetProfile_UserIdIsEmpty_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
            {
                var profile = await bot.GetProfile(string.Empty);
            });
        }

        [Fact]
        public async Task GetProfile_ErrorResponse_ThrowsException()
        {
            var httpClient = TestHttpClient.ThatReturnsAnError();

            var bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.GetProfile("test");
            });
        }

        [Fact]
        public async Task GetProfile_CorrectResponse_ReturnsUserProfile()
        {
            var httpClient = TestHttpClient.Create(JsonDocuments.UserProfile);

            var bot = TestConfiguration.CreateBot(httpClient);
            var profile = await bot.GetProfile("test");

            Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
            Assert.Equal("/profile/test", httpClient.RequestPath);

            Assert.NotNull(profile);
            Assert.Equal("LINE taro", profile.DisplayName);
            Assert.Equal(new Uri("http://obs.line-apps.com/..."), profile.PictureUrl);
            Assert.Equal("Hello, LINE!", profile.StatusMessage);
            Assert.Equal("Uxxxxxxxxxxxxxx...", profile.UserId);
        }

        [Fact]
        public async Task GetProfile_WithUser_ReturnsUserProfile()
        {
            var httpClient = TestHttpClient.Create(JsonDocuments.UserProfile);

            var bot = TestConfiguration.CreateBot(httpClient);
            var profile = await bot.GetProfile(new TestUser());

            Assert.Equal("/profile/testUser", httpClient.RequestPath);
            Assert.NotNull(profile);
        }
    }
}
