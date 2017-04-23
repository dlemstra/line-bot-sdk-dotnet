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
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("replyToken", async () =>
            {
                await bot.Reply((string)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Reply_ReplyTokenIsEmpty_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("replyToken", async () =>
            {
                await bot.Reply(string.Empty, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Reply_TokenIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("token", async () =>
            {
                await bot.Reply((IReplyToken)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Reply_MessagesIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Reply("token", null);
            });
        }

        [TestMethod]
        public async Task Reply_NoMessages_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messages", async () =>
            {
                await bot.Reply("token", new TextMessage[] { });
            });
        }

        [TestMethod]
        public async Task Reply_CorrectInput_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Reply("token", new TextMessage() { Text = "Test1" }, new TextMessage() { Text = "Test2" });

            string postedData = @"{""replyToken"":""token"",""messages"":[{""type"":""text"",""text"":""Test1""},{""type"":""text"",""text"":""Test2""}]}";

            Assert.AreEqual("/message/reply", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Reply_WithIReplyToken_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Reply(new TestMessage(), new TestTextMessage());

            string postedData = @"{""replyToken"":""testReplyToken"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/reply", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }
    }
}
