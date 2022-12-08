// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class PushTests
    {
        [Fact]
        public async Task Push_ToIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Push((string)null, new TextMessage() { Text = "Test" });
            });
        }

        [Fact]
        public async Task Push_ToIsEmpty_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("to", async () =>
            {
                await bot.Push(string.Empty, new TextMessage() { Text = "Test" });
            });
        }

        [Fact]
        public async Task Push_MessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push("id", null);
            });
        }

        [Fact]
        public async Task Push_EnumerableMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push("id", (IEnumerable<ISendMessage>)null);
            });
        }

        [Fact]
        public async Task Push_NoMessages_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messages", async () =>
            {
                await bot.Push("id", new TextMessage[] { });
            });
        }

        [Fact]
        public async Task Push_ErrorResponse_ThrowsException()
        {
            var httpClient = TestHttpClient.ThatReturnsAnError();

            var bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.Push("id", new TextMessage("Test"));
            });
        }

        [Fact]
        public async Task Push_CorrectInput_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push("id", new TextMessage() { Text = "Test" });

            var postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test""}]}";

            Assert.Equal("/message/push", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_WithEnumerable_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push("id", messages);

            var postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/push", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_GroupIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("group", async () =>
            {
                await bot.Push((IGroup)null, new TextMessage() { Text = "Test" });
            });
        }

        [Fact]
        public async Task Push_WithGroupAndMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestGroup(), null);
            });
        }

        [Fact]
        public async Task Push_WithGroupAndEnumerableMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestGroup(), (IEnumerable<ISendMessage>)null);
            });
        }

        [Fact]
        public async Task Push_WithGroup_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestGroup(), new TextMessage("FooBar"));

            var postedData = @"{""to"":""testGroup"",""messages"":[{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/push", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_WithGroupAndEnumerable_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestGroup(), messages);

            var postedData = @"{""to"":""testGroup"",""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/push", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_RoomIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("room", async () =>
            {
                await bot.Push((IRoom)null, new TextMessage() { Text = "Test" });
            });
        }

        [Fact]
        public async Task Push_WithRoomAndMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestRoom(), null);
            });
        }

        [Fact]
        public async Task Push_WithRoomAndEnumerableMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestRoom(), (IEnumerable<ISendMessage>)null);
            });
        }

        [Fact]
        public async Task Push_WithRoom_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestRoom(), new TextMessage("FooBar"));

            var postedData = @"{""to"":""testRoom"",""messages"":[{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/push", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_WithRoomAndEnumerable_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestRoom(), messages);

            var postedData = @"{""to"":""testRoom"",""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/push", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_UserIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
            {
                await bot.Push((IUser)null, new TextMessage() { Text = "Test" });
            });
        }

        [Fact]
        public async Task Push_WithUserAndMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestUser(), null);
            });
        }

        [Fact]
        public async Task Push_WithUserAndEnumerableMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestUser(), (IEnumerable<ISendMessage>)null);
            });
        }

        [Fact]
        public async Task Push_WithUser_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestUser(), new TextMessage("FooBar"));

            var postedData = @"{""to"":""testUser"",""messages"":[{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/push", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_WithUserAndEnumerable_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestUser(), messages);

            var postedData = @"{""to"":""testUser"",""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/push", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Push_With11Messages_CallsApi3Times()
        {
            var httpClient = TestHttpClient.Create();

            var messages = new TextMessage[11];
            for (var i = 0; i < messages.Length; i++)
                messages[i] = new TextMessage("Test" + i);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push("id", messages);

            var requests = httpClient.Requests.ToArray();
            Assert.Equal(3, requests.Length);

            var postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test0""},{""type"":""text"",""text"":""Test1""},{""type"":""text"",""text"":""Test2""},{""type"":""text"",""text"":""Test3""},{""type"":""text"",""text"":""Test4""}]}";

            Assert.Equal("/message/push", requests[0].RequestUri.PathAndQuery);
            Assert.Equal(postedData, requests[0].GetPostedData());

            postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test5""},{""type"":""text"",""text"":""Test6""},{""type"":""text"",""text"":""Test7""},{""type"":""text"",""text"":""Test8""},{""type"":""text"",""text"":""Test9""}]}";

            Assert.Equal("/message/push", requests[1].RequestUri.PathAndQuery);
            Assert.Equal(postedData, requests[1].GetPostedData());

            postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test10""}]}";

            Assert.Equal("/message/push", requests[2].RequestUri.PathAndQuery);
            Assert.Equal(postedData, requests[2].GetPostedData());
        }
    }
}
