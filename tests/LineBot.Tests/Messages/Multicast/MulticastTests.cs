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

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests.Messages.Multicast
{
    [TestClass]
    public class MulticastTests
    {
        [TestMethod]
        public async Task Multicast_ToIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<string>)null, new TextMessage("Test"));
            });
        }

        [TestMethod]
        public async Task Multicast_ToIsEmpty_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("to", async () =>
            {
                await bot.Multicast(Enumerable.Empty<string>(), new TextMessage("Test"));
            });
        }

        [TestMethod]
        public async Task Multicast_MessagesIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Multicast(new string[] { "id" }, null);
            });
        }

        [TestMethod]
        public async Task Multicast_NoMessages_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messages", async () =>
            {
                await bot.Multicast(new string[] { "id" }, new TextMessage[] { });
            });
        }

        [TestMethod]
        public async Task Multicast_EnumerableMessagesIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Multicast(new string[] { "id" }, (IEnumerable<ISendMessage>)null);
            });
        }

        [TestMethod]
        public async Task Multicast_CorrectInput_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new string[] { "id1", "id2" }, new TextMessage("Test"));

            string postedData = @"{""to"":[""id1"",""id2""],""messages"":[{""type"":""text"",""text"":""Test""}]}";

            Assert.AreEqual("/message/multicast", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Multicast_WithEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new string[] { "id" }, messages);

            string postedData = @"{""to"":[""id""],""messages"":[{""type"":""text"",""text"":""TestTextMessage""},{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/multicast", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Multicast_With6Messages_CallsApi2Times()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            TextMessage[] messages = new TextMessage[6];
            for (int i = 0; i < messages.Length; i++)
                messages[i] = new TextMessage("Test" + i);

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new string[] { "id1", "id2" }, messages);

            HttpRequestMessage[] requests = httpClient.Requests.ToArray();
            Assert.AreEqual(2, requests.Length);

            string postedData = @"{""to"":[""id1"",""id2""],""messages"":[{""type"":""text"",""text"":""Test0""},{""type"":""text"",""text"":""Test1""},{""type"":""text"",""text"":""Test2""},{""type"":""text"",""text"":""Test3""},{""type"":""text"",""text"":""Test4""}]}";

            Assert.AreEqual("/message/multicast", requests[0].RequestUri.PathAndQuery);
            Assert.AreEqual(postedData, requests[0].GetPostedData());

            postedData = @"{""to"":[""id1"",""id2""],""messages"":[{""type"":""text"",""text"":""Test5""}]}";

            Assert.AreEqual("/message/multicast", requests[1].RequestUri.PathAndQuery);
            Assert.AreEqual(postedData, requests[1].GetPostedData());
        }

        [TestMethod]
        public async Task Multicast_With201Ids_CallsApi2Times()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            string[] ids = new string[201];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = "id" + i;

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(ids, new TextMessage("Test"));

            string postedData = @"{""to"":[""id0"",""id1"",""id2"",""id3"",""id4"",""id5"",""id6"",""id7"",""id8"",""id9"",""id10"",""id11"",""id12"",""id13"",""id14"",""id15"",""id16"",""id17"",""id18"",""id19"",""id20"",""id21"",""id22"",""id23"",""id24"",""id25"",""id26"",""id27"",""id28"",""id29"",""id30"",""id31"",""id32"",""id33"",""id34"",""id35"",""id36"",""id37"",""id38"",""id39"",""id40"",""id41"",""id42"",""id43"",""id44"",""id45"",""id46"",""id47"",""id48"",""id49"",""id50"",""id51"",""id52"",""id53"",""id54"",""id55"",""id56"",""id57"",""id58"",""id59"",""id60"",""id61"",""id62"",""id63"",""id64"",""id65"",""id66"",""id67"",""id68"",""id69"",""id70"",""id71"",""id72"",""id73"",""id74"",""id75"",""id76"",""id77"",""id78"",""id79"",""id80"",""id81"",""id82"",""id83"",""id84"",""id85"",""id86"",""id87"",""id88"",""id89"",""id90"",""id91"",""id92"",""id93"",""id94"",""id95"",""id96"",""id97"",""id98"",""id99"",""id100"",""id101"",""id102"",""id103"",""id104"",""id105"",""id106"",""id107"",""id108"",""id109"",""id110"",""id111"",""id112"",""id113"",""id114"",""id115"",""id116"",""id117"",""id118"",""id119"",""id120"",""id121"",""id122"",""id123"",""id124"",""id125"",""id126"",""id127"",""id128"",""id129"",""id130"",""id131"",""id132"",""id133"",""id134"",""id135"",""id136"",""id137"",""id138"",""id139"",""id140"",""id141"",""id142"",""id143"",""id144"",""id145"",""id146"",""id147"",""id148"",""id149"",""id150"",""id151"",""id152"",""id153"",""id154"",""id155"",""id156"",""id157"",""id158"",""id159"",""id160"",""id161"",""id162"",""id163"",""id164"",""id165"",""id166"",""id167"",""id168"",""id169"",""id170"",""id171"",""id172"",""id173"",""id174"",""id175"",""id176"",""id177"",""id178"",""id179"",""id180"",""id181"",""id182"",""id183"",""id184"",""id185"",""id186"",""id187"",""id188"",""id189"",""id190"",""id191"",""id192"",""id193"",""id194"",""id195"",""id196"",""id197"",""id198"",""id199""],""messages"":[{""type"":""text"",""text"":""Test""}]}";

            HttpRequestMessage[] requests = httpClient.Requests.ToArray();
            Assert.AreEqual(2, requests.Length);

            Assert.AreEqual("/message/multicast", requests[0].RequestUri.PathAndQuery);
            Assert.AreEqual(postedData, requests[0].GetPostedData());

            postedData = @"{""to"":[""id200""],""messages"":[{""type"":""text"",""text"":""Test""}]}";

            Assert.AreEqual("/message/multicast", requests[1].RequestUri.PathAndQuery);
            Assert.AreEqual(postedData, requests[1].GetPostedData());
        }

        [TestMethod]
        public async Task Multicast_GroupIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<IGroup>)null, new TextMessage("Test"));
            });
        }

        [TestMethod]
        public async Task Multicast_GroupIsNullWithEnumerable_ThrowsException()
        {
            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<IGroup>)null, messages);
            });
        }

        [TestMethod]
        public async Task Multicast_WithGroup_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new TestGroup[] { new TestGroup() }, new TestTextMessage());

            string postedData = @"{""to"":[""testGroup""],""messages"":[{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/multicast", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Multicast_WithGroupAndEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new TestGroup[] { new TestGroup() }, messages);

            string postedData = @"{""to"":[""testGroup""],""messages"":[{""type"":""text"",""text"":""TestTextMessage""},{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/multicast", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Multicast_RoomIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<IRoom>)null, new TextMessage("Test"));
            });
        }

        [TestMethod]
        public async Task Multicast_RoomIsNullWithEnumerable_ThrowsException()
        {
            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<IRoom>)null, messages);
            });
        }

        [TestMethod]
        public async Task Multicast_WithRoom_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new TestRoom[] { new TestRoom() }, new TestTextMessage());

            string postedData = @"{""to"":[""testRoom""],""messages"":[{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/multicast", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Multicast_WithRoomAndEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new TestRoom[] { new TestRoom() }, messages);

            string postedData = @"{""to"":[""testRoom""],""messages"":[{""type"":""text"",""text"":""TestTextMessage""},{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/multicast", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Multicast_UserIsNull_ThrowsException()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<IUser>)null, new TextMessage("Test"));
            });
        }

        [TestMethod]
        public async Task Multicast_UserIsNullWithEnumerable_ThrowsException()
        {
            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = new LineBot(Configuration.ForTest, null);
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<IUser>)null, messages);
            });
        }

        [TestMethod]
        public async Task Multicast_WithUser_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new TestUser[] { new TestUser() }, new TestTextMessage());

            string postedData = @"{""to"":[""testUser""],""messages"":[{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/multicast", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }

        [TestMethod]
        public async Task Multicast_WithUserAndEnumerable_CallsApi()
        {
            TestHttpClient httpClient = TestHttpClient.Create();

            IEnumerable<TestTextMessage> messages = Enumerable.Repeat(new TestTextMessage(), 2);

            ILineBot bot = new LineBot(Configuration.ForTest, httpClient);
            await bot.Multicast(new TestUser[] { new TestUser() }, messages);

            string postedData = @"{""to"":[""testUser""],""messages"":[{""type"":""text"",""text"":""TestTextMessage""},{""type"":""text"",""text"":""TestTextMessage""}]}";

            Assert.AreEqual("/message/multicast", httpClient.RequestPath);
            Assert.AreEqual(postedData, httpClient.PostedData);
        }
    }
}
