// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class LineEventTests
    {
        [TestMethod]
        [DeploymentItem(JsonDocuments.EmptyObject)]
        public async Task GetEvents_EmptyObject_ReturnsEmptyEnumerable()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.EmptyObject);

            var events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(0, events.Count());
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.NoEvents)]
        public async Task GetEvents_NoEvents_ReturnsEmptyEnumerable()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.NoEvents);

            var events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(0, events.Count());
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Whitespace)]
        public async Task GetEvents_Whitespace_ReturnsEmptyEnumerable()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Whitespace);

            var events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(0, events.Count());
        }

        [TestMethod]
        public async Task GetEvents_NoData_ReturnsEmptyEnumerable()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest();

            var events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(0, events.Count());
        }
    }
}