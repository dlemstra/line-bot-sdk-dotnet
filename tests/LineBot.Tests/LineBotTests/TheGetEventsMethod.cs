// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        [DeploymentItem(JsonDocuments.Events.Webhook)]
        public class TheGetEventsMethod
        {
            [TestMethod]
            public async Task ShouldLogRequest()
            {
                var logger = new TestLogger();
                var bot = TestConfiguration.CreateBot(logger);
                var request = new TestHttpRequest(JsonDocuments.Events.Webhook);

                await bot.GetEvents(request);

                var actual = Encoding.UTF8.GetString(logger.LogReceivedEventsEventsData);

                var bytes = await File.ReadAllBytesAsync(JsonDocuments.Events.Webhook);
                var expected = Encoding.UTF8.GetString(bytes).Substring(1); // Skip preamable.

                Assert.AreEqual(expected, actual);
            }

            [TestMethod]
            public async Task ShouldHaveDestination()
            {
                var bot = TestConfiguration.CreateBot();
                var request = new TestHttpRequest(JsonDocuments.Events.Webhook);

                var events = await bot.GetEvents(request);

                Assert.AreEqual("xxxxxxxxxx", events.Destination);
            }

            [TestMethod]
            public async Task ShouldHaveDestinationWhenEventsNull()
            {
                var bot = TestConfiguration.CreateBot();
                var request = new TestHttpRequest(JsonDocuments.Events.NoEvents);

                var events = await bot.GetEvents(request);

                Assert.IsNotNull(events.Events);
                Assert.AreEqual(0, events.Events.Count());
                Assert.AreEqual("xxxxxxxxxx", events.Destination);
            }
        }
    }
}