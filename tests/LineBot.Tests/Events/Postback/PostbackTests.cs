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
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class PostbackTests
    {
        private const string InvalidJson = "Events\\Invalid.json";
        private const string PostbackEventJson = "Events\\Postback\\PostbackEvent.json";
        private const string PostbackEventWithoutPostbackJson = "Events\\Postback\\PostbackEventWithoutPostback.json";

        [TestMethod]
        [DeploymentItem(PostbackEventJson)]
        public async Task GetEvents_ValidRequest_ReturnsPostbackEvent()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(PostbackEventJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Postback, lineEvent.EventType);

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            IPostback postback = lineEvent.Postback;
            Assert.IsNotNull(postback);
            Assert.AreEqual("action=buyItem&itemId=123123&color=red", postback.Data);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", postback.ReplyToken);
        }

        [TestMethod]
        [DeploymentItem(PostbackEventWithoutPostbackJson)]
        public async Task GetEvents_RequestWithoutPostback_PostbackIsNull()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(PostbackEventWithoutPostbackJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Postback, lineEvent.EventType);
            Assert.IsNull(lineEvent.Postback);
        }

        [TestMethod]
        [DeploymentItem(InvalidJson)]
        public async Task GetEvents_InvalidRequest_PostbackIsNull()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(InvalidJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNull(lineEvent.Postback);
        }
    }
}
