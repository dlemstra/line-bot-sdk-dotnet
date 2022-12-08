// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class PostbackTests
    {
        [Fact]
        public async Task GetEvents_ValidRequest_ReturnsPostbackEvent()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Postback);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Equal(LineEventType.Postback, lineEvent.EventType);

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            var postback = lineEvent.Postback;
            Assert.NotNull(postback);
            Assert.Equal("action=buyItem&itemId=123123&color=red", postback.Data);
            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", postback.ReplyToken);
        }

        [Fact]
        public async Task GetEvents_RequestWithoutPostback_PostbackIsNull()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.WithoutPostback);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Equal(LineEventType.Postback, lineEvent.EventType);
            Assert.Null(lineEvent.Postback);
        }

        [Fact]
        public async Task GetEvents_InvalidRequest_PostbackIsNull()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Null(lineEvent.Postback);
        }
    }
}
