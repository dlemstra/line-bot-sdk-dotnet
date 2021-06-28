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
        public class TheLeaveRoomMethod
        {
            [TestMethod]
            public async Task ThrowsExceptionWhenRoomsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("room", async () =>
                {
                    await bot.LeaveRoom((IRoom)null);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenRoomIdIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("roomId", async () =>
                {
                    await bot.LeaveRoom((string)null);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionRoomIsEmpty()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("roomId", async () =>
                {
                    await bot.LeaveRoom(string.Empty);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenResponseIsError()
            {
                TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

                ILineBot bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.LeaveRoom("test");
                });
            }

            [TestMethod]
            public async Task ShouldCallsApiWithRoomId()
            {
                TestHttpClient httpClient = TestHttpClient.Create();

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                await bot.LeaveRoom("test");

                Assert.AreEqual(HttpMethod.Post, httpClient.RequestMethod);
                Assert.AreEqual("/room/test/leave", httpClient.RequestPath);
            }

            [TestMethod]
            public async Task ShouldCallsApiWithRoom()
            {
                TestHttpClient httpClient = TestHttpClient.Create();

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                await bot.LeaveRoom(new TestRoom());

                Assert.AreEqual("/room/testRoom/leave", httpClient.RequestPath);
            }

            [TestMethod]
            public async Task ShouldReturnTheInstance()
            {
                var httpClient = TestHttpClient.Create();
                var bot = TestConfiguration.CreateBot(httpClient);

                var result = await bot.LeaveRoom("test");
                Assert.AreSame(bot, result);
            }
        }
    }
}
