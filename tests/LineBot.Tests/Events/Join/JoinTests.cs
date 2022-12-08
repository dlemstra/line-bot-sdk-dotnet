// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class JoinTests
    {
        [Fact]
        public async Task GetEvents_ValidRequest_ReturnsFollowEvent()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Join);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Equal(LineEventType.Join, lineEvent.EventType);

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.Group, source.SourceType);
            Assert.Equal("cxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", source.Group.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);
        }
    }
}
