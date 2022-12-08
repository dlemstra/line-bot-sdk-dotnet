// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class ReplyTokenTests
    {
        [Fact]
        public async Task GetEvents_InvalidRequest_ReplyTokenReturnsNull()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Null(lineEvent.ReplyToken);
        }
    }
}
