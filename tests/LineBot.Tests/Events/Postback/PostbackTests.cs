// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class PostbackTests
    {
        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Postback)]
        public async Task GetEvents_ValidRequest_ReturnsPostbackEvent()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Postback);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Postback, lineEvent.EventType);

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            IPostback postback = lineEvent.Postback;
            Assert.IsNotNull(postback);
            Assert.AreEqual("action=buyItem&itemId=123123&color=red", postback.Data);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", postback.ReplyToken);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.WithoutPostback)]
        public async Task GetEvents_RequestWithoutPostback_PostbackIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.WithoutPostback);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Postback, lineEvent.EventType);
            Assert.IsNull(lineEvent.Postback);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Invalid)]
        public async Task GetEvents_InvalidRequest_PostbackIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNull(lineEvent.Postback);
        }
    }
}
