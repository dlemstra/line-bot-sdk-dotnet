// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheGetMessageContentMethod
        {
            [Fact]
            public async Task ThrowsExceptionWhenMessageIsNull()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("message", async () =>
                {
                    await bot.GetMessageContent((IMessage)null);
                });
            }

            [Fact]
            public async Task ThrowsExceptionWhenMessageIdIsNull()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messageId", async () =>
                {
                    await bot.GetMessageContent((string)null);
                });
            }

            [Fact]
            public async Task ThrowsExceptionWhenMessageIsEmpty()
            {
                var bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messageId", async () =>
                {
                    await bot.GetMessageContent(string.Empty);
                });
            }

            [Fact]
            public async Task ThrowsExceptionWhenResponseIsError()
            {
                var httpClient = TestHttpClient.ThatReturnsAnError();

                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.GetMessageContent("test");
                });
            }

            [Fact]
            public async Task ReturnsNullWhenResponseIsEmpty()
            {
                var httpClient = TestHttpClient.Create();

                var bot = TestConfiguration.CreateBot(httpClient);
                var data = await bot.GetMessageContent("test");

                Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
                Assert.Equal("/message/test/content", httpClient.RequestPath);

                Assert.Null(data);
            }

            [Fact]
            public async Task ReturnsDataWhenWithCorrectMessageId()
            {
                var input = new byte[12] { 68, 105, 114, 107, 32, 76, 101, 109, 115, 116, 114, 97 };

                var httpClient = TestHttpClient.ThatReturnsData(input);

                var bot = TestConfiguration.CreateBot(httpClient);
                var data = await bot.GetMessageContent("test");

                Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
                Assert.Equal("/message/test/content", httpClient.RequestPath);

                Assert.NotNull(data);
                Assert.Equal(data, input);
            }

            [Fact]
            public async Task ReturnsDataWithCorrectMessage()
            {
                var input = new byte[12] { 68, 105, 114, 107, 32, 76, 101, 109, 115, 116, 114, 97 };

                var httpClient = TestHttpClient.ThatReturnsData(input);

                var bot = TestConfiguration.CreateBot(httpClient);
                var data = await bot.GetMessageContent(new TestMessage(MessageType.Image));

                Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
                Assert.Equal("/message/testMessage/content", httpClient.RequestPath);

                Assert.NotNull(data);
                Assert.Equal(data, input);
            }
        }
    }
}
