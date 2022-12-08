// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class ReplyTests
    {
        [Fact]
        public async Task Reply_ReplyTokenIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("replyToken", async () =>
            {
                await bot.Reply((string)null, new TextMessage() { Text = "Test" });
            });
        }

        [Fact]
        public async Task Reply_ReplyTokenIsEmpty_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("replyToken", async () =>
            {
                await bot.Reply(string.Empty, new TextMessage() { Text = "Test" });
            });
        }

        [Fact]
        public async Task Reply_TokenIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("token", async () =>
            {
                await bot.Reply((IReplyToken)null, new TextMessage() { Text = "Test" });
            });
        }

        [Fact]
        public async Task Reply_MessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Reply("token", null);
            });
        }

        [Fact]
        public async Task Reply_EnumerableMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Reply("token", (IEnumerable<ISendMessage>)null);
            });
        }

        [Fact]
        public async Task Reply_NoMessages_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messages", async () =>
            {
                await bot.Reply("token", new TextMessage[] { });
            });
        }

        [Fact]
        public async Task Reply_ErrorResponse_ThrowsException()
        {
            var httpClient = TestHttpClient.ThatReturnsAnError();

            var bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.Reply("token", new TextMessage("Test"));
            });
        }

        [Fact]
        public async Task Reply_TooManyMessages_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();

            await ExceptionAssert.ThrowsAsync<InvalidOperationException>("The maximum number of messages is 5.", async () =>
            {
                await bot.Reply("id", new ISendMessage[6]);
            });
        }

        [Fact]
        public async Task Reply_CorrectInput_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Reply("token", new TextMessage("Test1"), new TextMessage("Test2"));

            var postedData = @"{""replyToken"":""token"",""messages"":[{""type"":""text"",""text"":""Test1""},{""type"":""text"",""text"":""Test2""}]}";

            Assert.Equal("/message/reply", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_WithEnumerable_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Reply("token", messages);

            var postedData = @"{""replyToken"":""token"",""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/reply", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Reply_WithTokenAndEnumerableMessagesIsNull_CallsApi()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Reply(new TestMessage(), (IEnumerable<ISendMessage>)null);
            });
        }

        [Fact]
        public async Task Reply_WithToken_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Reply(new TestMessage(), new TextMessage("FooBar"));

            var postedData = @"{""replyToken"":""testReplyToken"",""messages"":[{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/reply", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Reply_WithTokenAndEnumerable_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Reply(new TestMessage(), messages);

            var postedData = @"{""replyToken"":""testReplyToken"",""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/reply", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }
    }
}
