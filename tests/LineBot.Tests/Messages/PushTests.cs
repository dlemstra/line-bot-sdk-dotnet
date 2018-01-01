// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class PushTests
    {
        [TestMethod]
        public async Task Push_ToIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Push((string)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Push_ToIsEmpty_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("to", async () =>
            {
                await bot.Push(string.Empty, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Push_MessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push("id", null);
            });
        }

        [TestMethod]
        public async Task Push_EnumerableMessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push("id", (IEnumerable<ISendMessage>)null);
            });
        }

        [TestMethod]
        public async Task Push_NoMessages_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messages", async () =>
            {
                await bot.Push("id", new TextMessage[] { });
            });
        }

        [TestMethod]
        public async Task Push_ErrorResponse_ThrowsException()
        {
            TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.Push("id", new TextMessage("Test"));
            });
        }

        [TestMethod]
        public async Task Push_CorrectInput_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push("id", new TextMessage() { Text = "Test" });

            string postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test""}]}";

            Assert.AreEqual("/message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_WithEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push("id", messages);

            string postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""},{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_GroupIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("group", async () =>
            {
                await bot.Push((IGroup)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Push_WithGroupAndMessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestGroup(), null);
            });
        }

        [TestMethod]
        public async Task Push_WithGroupAndEnumerableMessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestGroup(), (IEnumerable<ISendMessage>)null);
            });
        }

        [TestMethod]
        public async Task Push_WithGroup_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestGroup(), new TestTextMessage());

            string postedData = @"{""to"":""testGroup"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_WithGroupAndEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestGroup(), messages);

            string postedData = @"{""to"":""testGroup"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""},{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_RoomIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("room", async () =>
            {
                await bot.Push((IRoom)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Push_WithRoomAndMessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestRoom(), null);
            });
        }

        [TestMethod]
        public async Task Push_WithRoomAndEnumerableMessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestRoom(), (IEnumerable<ISendMessage>)null);
            });
        }

        [TestMethod]
        public async Task Push_WithRoom_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestRoom(), new TestTextMessage());

            string postedData = @"{""to"":""testRoom"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_WithRoomAndEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestRoom(), messages);

            string postedData = @"{""to"":""testRoom"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""},{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_UserIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
            {
                await bot.Push((IUser)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Push_WithUserAndMessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestUser(), null);
            });
        }

        [TestMethod]
        public async Task Push_WithUserAndEnumerableMessagesIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push(new TestUser(), (IEnumerable<ISendMessage>)null);
            });
        }

        [TestMethod]
        public async Task Push_WithUser_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestUser(), new TestTextMessage());

            string postedData = @"{""to"":""testUser"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_WithUserAndEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push(new TestUser(), messages);

            string postedData = @"{""to"":""testUser"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""},{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Push_With11Messages_CallsApi3Times()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            TextMessage[] messages = new TextMessage[11];
            for (int i = 0; i < messages.Length; i++)
                messages[i] = new TextMessage("Test" + i);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            await bot.Push("id", messages);

            HttpRequestMessage[] requests = httpClient.Requests.ToArray();
            Assert.AreEqual(3, requests.Length);

            string postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test0""},{""type"":""text"",""text"":""Test1""},{""type"":""text"",""text"":""Test2""},{""type"":""text"",""text"":""Test3""},{""type"":""text"",""text"":""Test4""}]}";

            Assert.AreEqual("/message/push", requests[0].RequestUri.PathAndQuery);
            Assert.AreEqual(postedData, requests[0].GetPostedData());

            postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test5""},{""type"":""text"",""text"":""Test6""},{""type"":""text"",""text"":""Test7""},{""type"":""text"",""text"":""Test8""},{""type"":""text"",""text"":""Test9""}]}";

            Assert.AreEqual("/message/push", requests[1].RequestUri.PathAndQuery);
            Assert.AreEqual(postedData, requests[1].GetPostedData());

            postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test10""}]}";

            Assert.AreEqual("/message/push", requests[2].RequestUri.PathAndQuery);
            Assert.AreEqual(postedData, requests[2].GetPostedData());
        }
    }
}
