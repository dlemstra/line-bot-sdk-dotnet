// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheGetEventsMethod
        {
            [Fact]
            public async Task ShouldLogRequest()
            {
                var logger = new TestLogger();
                var bot = TestConfiguration.CreateBot(logger);
                var request = new TestHttpRequest(JsonDocuments.Events.Webhook);

                await bot.GetEvents(request);

                var actual = Encoding.UTF8.GetString(logger.LogReceivedEventsEventsData);

                var bytes = await File.ReadAllBytesAsync(JsonDocuments.Events.Webhook);
                var expected = Encoding.UTF8.GetString(bytes).Substring(1); // Skip preamable.

                Assert.Equal(expected, actual);
            }

            [Fact]
            public async Task ShouldHaveDestination()
            {
                var bot = TestConfiguration.CreateBot();
                var request = new TestHttpRequest(JsonDocuments.Events.Webhook);

                var events = await bot.GetEvents(request);

                Assert.Equal("xxxxxxxxxx", events.Destination);
            }

            [Fact]
            public async Task ShouldHaveDestinationWhenEventsNull()
            {
                var bot = TestConfiguration.CreateBot();
                var request = new TestHttpRequest(JsonDocuments.Events.NoEvents);

                var events = await bot.GetEvents(request);

                Assert.NotNull(events.Events);
                Assert.Empty(events.Events);
                Assert.Equal("xxxxxxxxxx", events.Destination);
            }
        }
    }
}
