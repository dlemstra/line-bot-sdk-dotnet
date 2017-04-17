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
    public class MessageTests
    {
        private const string ImageJson = "Events\\Messages\\Image.json";
        private const string InvalidJson = "Events\\Invalid.json";
        private const string InvalidMesssageJson = "Events\\Messages\\InvalidMessage.json";
        private const string MessageEventWithoutMessageJson = "Events\\Messages\\MessageEventWithoutMessage.json";
        private const string TextJson = "Events\\Messages\\Text.json";

        [TestMethod]
        [DeploymentItem(MessageEventWithoutMessageJson)]
        public async Task GetEvents_RequestWithoutMessage_MessageIsNull()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(MessageEventWithoutMessageJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Message, lineEvent.EventType);
            Assert.IsNull(lineEvent.Message);
        }

        [TestMethod]
        [DeploymentItem(InvalidMesssageJson)]
        public async Task GetEvents_InvalidMessageType_MessageTypeIsUnknown()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(InvalidMesssageJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual(MessageType.Unknown, lineEvent.Message.MessageType);
        }

        [TestMethod]
        [DeploymentItem(InvalidJson)]
        public async Task GetEvents_InvalidRequest_MessageIsNull()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(InvalidJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNull(lineEvent.Message);
        }

        [TestMethod]
        [DeploymentItem(TextJson)]
        public async Task Group_MessageTypeIsText_ReturnsMessage()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(TextJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual(MessageType.Text, lineEvent.Message.MessageType);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.AreEqual("Hello, world", lineEvent.Message.Text);
        }

        [TestMethod]
        [DeploymentItem(ImageJson)]
        public async Task Group_MessageTypeIsImage_ReturnsMessage()
        {
            ILineBot bot = new LineBot(Configuration.ForTest, null);
            TestHttpRequest request = new TestHttpRequest(ImageJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual(MessageType.Image, lineEvent.Message.MessageType);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Text);
        }
    }
}
