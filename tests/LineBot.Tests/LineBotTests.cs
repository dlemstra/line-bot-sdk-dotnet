// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class LineBotTests
    {
        [TestMethod]
        public void Create_ConfigurationIsNull_ThrowsArgumentNullException()
        {
            ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
            {
                new LineBot(null);
            });
        }

        [TestMethod]
        public async Task GetContent_MessageIdIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messageId", async () =>
            {
                await bot.GetContent(null);
            });
        }

        [TestMethod]
        public async Task GetContent_MessageIsEmpty_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messageId", async () =>
            {
                await bot.GetContent(string.Empty);
            });
        }

        [TestMethod]
        public async Task GetContent_ErrorResponse_ThrowsException()
        {
            TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.GetContent("test");
            });
        }

        [TestMethod]
        public async Task GetContent_EmptyResponse_ReturnsNull()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            byte[] data = await bot.GetContent("test");

            Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
            Assert.AreEqual("message/test/content", httpClient.RequestPath);

            Assert.IsNull(data);
        }

        [TestMethod]
        public async Task GetContent_CorrectResponse_ReturnsData()
        {
            byte[] input = new byte[12] { 68, 105, 114, 107, 32, 76, 101, 109, 115, 116, 114, 97 };

            TestHttpClient httpClient = TestHttpClient.ThatReturnsData(input);

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            byte[] data = await bot.GetContent("test");

            Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
            Assert.AreEqual("message/test/content", httpClient.RequestPath);

            Assert.IsNotNull(data);
            CollectionAssert.AreEqual(data, input);
        }

        [TestMethod]
        public async Task LeaveGroup_GroupIdIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("groupId", async () =>
            {
                await bot.LeaveGroup(null);
            });
        }

        [TestMethod]
        public async Task LeaveGroup_GroupIsEmpty_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("groupId", async () =>
            {
                await bot.LeaveGroup(string.Empty);
            });
        }

        [TestMethod]
        public async Task LeaveGroup_ErrorResponse_ThrowsException()
        {
            TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.LeaveGroup("test");
            });
        }

        [TestMethod]
        public async Task LeaveGroup_CorrectResponse_ThrowsNoException()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.LeaveGroup("test");

            Assert.AreEqual(HttpMethod.Post, httpClient.RequestMethod);
            Assert.AreEqual("group/test/leave", httpClient.RequestPath);
        }

        [TestMethod]
        public async Task LeaveRoom_RoomIdIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("roomId", async () =>
            {
                await bot.LeaveRoom(null);
            });
        }

        [TestMethod]
        public async Task LeaveRoom_RoomIsEmpty_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("roomId", async () =>
            {
                await bot.LeaveRoom(string.Empty);
            });
        }

        [TestMethod]
        public async Task LeaveRoom_ErrorResponse_ThrowsException()
        {
            TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.LeaveRoom("test");
            });
        }

        [TestMethod]
        public async Task LeaveRoom_CorrectResponse_ThrowsNoException()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.LeaveRoom("test");

            Assert.AreEqual(HttpMethod.Post, httpClient.RequestMethod);
            Assert.AreEqual("room/test/leave", httpClient.RequestPath);
        }
    }
}
