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

namespace Line.Tests.Messages.Push
{
    [TestClass]
    public class PushTests
    {
        [TestMethod]
        public async Task Push_IdIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Push((string)null, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Push_IdIsEmpty_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("to", async () =>
            {
                await bot.Push(string.Empty, new TextMessage() { Text = "Test" });
            });
        }

        [TestMethod]
        public async Task Push_MessagesIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Push("id", null);
            });
        }

        [TestMethod]
        public async Task Push_NoMessages_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messages", async () =>
            {
                await bot.Push("id", new TextMessage[] { });
            });
        }

        [TestMethod]
        public async Task Push_CorrectInput_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Push("id", new TextMessage() { Text = "Test" });

            string postedData = @"{""to"":""id"",""messages"":[{""type"":""text"",""text"":""Test""}]}";

            Assert.AreEqual("message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Reply_WithIGroup_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Push(new TestGroup(), new TestTextMessage());

            string postedData = @"{""to"":""testGroup"",""messages"":[{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("message/push", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }
    }
}
