﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class FollowTests
    {
        [Fact]
        public async Task GetEvents_ValidRequest_ReturnsFollowEvent()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Follow);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Equal(LineEventType.Follow, lineEvent.EventType);

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);
        }
    }
}
