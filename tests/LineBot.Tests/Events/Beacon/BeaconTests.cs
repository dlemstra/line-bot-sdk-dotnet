// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
