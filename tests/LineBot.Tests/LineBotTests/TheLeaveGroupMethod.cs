// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheLeaveGroupMethod
        {
            [Fact]
            public async Task ThrowsExceptionWhenGroupIsNull()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("group", async () =>
                {
                    await bot.LeaveGroup((IGroup)null);
                });
            }

            [Fact]
            public async Task ThrowsExceptionWhenGroupIdIsNull()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("groupId", async () =>
                {
                    await bot.LeaveGroup((string)null);
                });
            }

            [Fact]
            public async Task ThrowsExceptionWhenGroupIsEmpty()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("groupId", async () =>
                {
                    await bot.LeaveGroup(string.Empty);
                });
            }

            [Fact]
            public async Task ThrowsExceptionWhenResponseIsError()
            {
                var httpClient = TestHttpClient.ThatReturnsAnError();

                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.LeaveGroup("test");
                });
            }

            [Fact]
            public async Task ShouldCallsApiWithGroupId()
            {
                var httpClient = TestHttpClient.Create();

                var bot = TestConfiguration.CreateBot(httpClient);
                await bot.LeaveGroup("test");

                Assert.Equal(HttpMethod.Post, httpClient.RequestMethod);
                Assert.Equal("/group/test/leave", httpClient.RequestPath);
            }

            [Fact]
            public async Task ShouldCallsApiWithGroup()
            {
                var httpClient = TestHttpClient.Create();

                var bot = TestConfiguration.CreateBot(httpClient);
                await bot.LeaveGroup(new TestGroup());

                Assert.Equal("/group/testGroup/leave", httpClient.RequestPath);
            }

            [Fact]
            public async Task ShouldReturnTheInstance()
            {
                var httpClient = TestHttpClient.Create();
                var bot = TestConfiguration.CreateBot(httpClient);

                var result = await bot.LeaveGroup("test");
                Assert.Same(bot, result);
            }
        }
    }
}
