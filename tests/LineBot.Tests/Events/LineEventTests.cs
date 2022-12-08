// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class LineEventTests
    {
        [Fact]
        public async Task GetEvents_EmptyObject_ReturnsEmptyEnumerable()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.EmptyObject);

            var events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Empty(events);
        }

        [Fact]
        public async Task GetEvents_NoEvents_ReturnsEmptyEnumerable()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.NoEvents);

            var events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Empty(events);
        }

        [Fact]
        public async Task GetEvents_Whitespace_ReturnsEmptyEnumerable()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Whitespace);

            var events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Empty(events);
        }

        [Fact]
        public async Task GetEvents_NoData_ReturnsEmptyEnumerable()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest();

            var events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Empty(events);
        }
    }
}
