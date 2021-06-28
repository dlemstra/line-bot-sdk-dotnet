// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class ReplyTests
    {
        [TestMethod]
        public async Task Reply_ReplyTokenIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("replyToken", async () =>
            {
                await bot.Reply((string)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Reply_ReplyTokenIsEmpty_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("replyToken", async () =>
            {
                await bot.Reply(string.Empty, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Reply_TokenIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("token", async () =>
            {
                await bot.Reply((IReplyToken)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Reply_MessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Reply("token", null);
            });
        }

        [TestMethod]
        public async Task Reply_EnumerableMessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Reply("token", (IEnumerable<ISendMessage>)null);
            });
        }

        [TestMethod]
        public async Task Reply_NoMessages_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messages", async () =>
            {
                await bot.Reply("token", new TextMessage[] { });
            });
        }

        [TestMethod]
        public async Task Reply_ErrorResponse_ThrowsException()
        {
            TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.Reply("token", new TextMessage("Test"));
            });
        }

        [TestMethod]
        public async Task Reply_TooManyMessages_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();

            await ExceptionAssert.ThrowsAsync<InvalidOperationException>("The maximum number of messages is 5.", async () =>
            {
                await bot.Reply("id", new ISendMessage[6]);
            });
        }

        [TestMethod]
        public async Task Reply_CorrectInput_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Reply("token", new TextMessage("Test1"), new TextMessage("Test2"));

            string postedData = @"{""replyToken"":""token"",""messages"":[{""type"":""text"",""text"":""Test1""},{""type"":""text"",""text"":""Test2""}]}";

            Assert.AreEqual("/message/reply", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_WithEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TextMessage> messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Reply("token", messages);

            string postedData = @"{""replyToken"":""token"",""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.AreEqual("/message/reply", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Reply_WithTokenAndEnumerableMessagesIsNull_CallsApi()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Reply(new TestMessage(), (IEnumerable<ISendMessage>)null);
            });
        }

        [TestMethod]
        public async Task Reply_WithToken_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Reply(new TestMessage(), new TextMessage("FooBar"));

            string postedData = @"{""replyToken"":""testReplyToken"",""messages"":[{""type"":""text"",""text"":""FooBar""}]}";

            Assert.AreEqual("/message/reply", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Reply_WithTokenAndEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TextMessage> messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Reply(new TestMessage(), messages);

            string postedData = @"{""replyToken"":""testReplyToken"",""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.AreEqual("/message/reply", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }
    }
}
