// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheLeaveRoomMethod
        {
            [Fact]
            public async Task ThrowsExceptionWhenRoomsNull()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("room", async () =>
                {
                    await bot.LeaveRoom((IRoom)null);
                });
            }

            [Fact]
            public async Task ThrowsExceptionWhenRoomIdIsNull()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("roomId", async () =>
                {
                    await bot.LeaveRoom((string)null);
                });
            }

            [Fact]
            public async Task ThrowsExceptionRoomIsEmpty()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("roomId", async () =>
                {
                    await bot.LeaveRoom(string.Empty);
                });
            }

            [Fact]
            public async Task ThrowsExceptionWhenResponseIsError()
            {
                var httpClient = TestHttpClient.ThatReturnsAnError();

                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.LeaveRoom("test");
                });
            }

            [Fact]
            public async Task ShouldCallsApiWithRoomId()
            {
                var httpClient = TestHttpClient.Create();

                var bot = TestConfiguration.CreateBot(httpClient);
                await bot.LeaveRoom("test");

                Assert.Equal(HttpMethod.Post, httpClient.RequestMethod);
                Assert.Equal("/room/test/leave", httpClient.RequestPath);
            }

            [Fact]
            public async Task ShouldCallsApiWithRoom()
            {
                var httpClient = TestHttpClient.Create();

                var bot = TestConfiguration.CreateBot(httpClient);
                await bot.LeaveRoom(new TestRoom());

                Assert.Equal("/room/testRoom/leave", httpClient.RequestPath);
            }

            [Fact]
            public async Task ShouldReturnTheInstance()
            {
                var httpClient = TestHttpClient.Create();
                var bot = TestConfiguration.CreateBot(httpClient);

                var result = await bot.LeaveRoom("test");
                Assert.Same(bot, result);
            }
        }
    }
}
