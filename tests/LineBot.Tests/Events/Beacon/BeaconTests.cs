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
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class BeaconTests
    {
        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Beacon)]
        public async Task GetEvents_ValidRequest_ReturnsBeaconEvent()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Beacon);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Beacon, lineEvent.EventType);

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            IBeacon beacon = lineEvent.Beacon;
            Assert.IsNotNull(beacon);
            Assert.AreEqual("d41d8cd98f", beacon.Hwid);
            Assert.AreEqual(BeaconType.Enter, beacon.BeaconType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", beacon.ReplyToken);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.WithoutBeacon)]
        public async Task GetEvents_RequestWithoutBeacon_BeaconIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.WithoutBeacon);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Beacon, lineEvent.EventType);
            Assert.IsNull(lineEvent.Beacon);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.InvalidBeacon)]
        public async Task GetEvents_InvalidBeaconType_BeaconTypeIsUnknown()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.InvalidBeacon);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNotNull(lineEvent.Beacon);
            Assert.AreEqual(BeaconType.Unknown, lineEvent.Beacon.BeaconType);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Invalid)]
        public async Task GetEvents_InvalidRequest_BeaconIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNull(lineEvent.Beacon);
        }
    }
}
