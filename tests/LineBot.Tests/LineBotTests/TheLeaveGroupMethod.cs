// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheLeaveGroupMethod
        {
            [TestMethod]
            public async Task ThrowsExceptionWhenGroupIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("group", async () =>
                {
                    await bot.LeaveGroup((IGroup)null);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenGroupIdIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("groupId", async () =>
                {
                    await bot.LeaveGroup((string)null);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenGroupIsEmpty()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("groupId", async () =>
                {
                    await bot.LeaveGroup(string.Empty);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenResponseIsError()
            {
                TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

                ILineBot bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.LeaveGroup("test");
                });
            }

            [TestMethod]
            public async Task ShouldCallsApiWithGroupId()
            {
                TestHttpClient httpClient = TestHttpClient.Create();

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                await bot.LeaveGroup("test");

                Assert.AreEqual(HttpMethod.Post, httpClient.RequestMethod);
                Assert.AreEqual("/group/test/leave", httpClient.RequestPath);
            }

            [TestMethod]
            public async Task ShouldCallsApiWithGroup()
            {
                TestHttpClient httpClient = TestHttpClient.Create();

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                await bot.LeaveGroup(new TestGroup());

                Assert.AreEqual("/group/testGroup/leave", httpClient.RequestPath);
            }

            [TestMethod]
            public async Task ShouldReturnTheInstance()
            {
                var httpClient = TestHttpClient.Create();
                var bot = TestConfiguration.CreateBot(httpClient);

                var result = await bot.LeaveGroup("test");
                Assert.AreSame(bot, result);
            }
        }
    }
}
