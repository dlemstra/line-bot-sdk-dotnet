// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class BeaconTests
    {
        [Fact]
        public async Task GetEvents_ValidRequest_ReturnsBeaconEvent()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Beacon);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Equal(LineEventType.Beacon, lineEvent.EventType);

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            var beacon = lineEvent.Beacon;
            Assert.NotNull(beacon);
            Assert.Equal("d41d8cd98f", beacon.Hwid);
            Assert.Equal(BeaconType.Enter, beacon.BeaconType);
            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", beacon.ReplyToken);
        }

        [Fact]
        public async Task GetEvents_RequestWithoutBeacon_BeaconIsNull()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.WithoutBeacon);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Equal(LineEventType.Beacon, lineEvent.EventType);
            Assert.Null(lineEvent.Beacon);
        }

        [Fact]
        public async Task GetEvents_InvalidBeaconType_BeaconTypeIsUnknown()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.InvalidBeacon);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.NotNull(lineEvent.Beacon);
            Assert.Equal(BeaconType.Unknown, lineEvent.Beacon.BeaconType);
        }

        [Fact]
        public async Task GetEvents_InvalidRequest_BeaconIsNull()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Null(lineEvent.Beacon);
        }
    }
}
