// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.WithoutMessage)]
        public async Task GetEvents_RequestWithoutMessage_MessageIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.WithoutMessage);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Message, lineEvent.EventType);
            Assert.IsNull(lineEvent.Message);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Messages.Invalid)]
        public async Task GetEvents_InvalidMessageType_MessageTypeIsUnknown()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Messages.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual(MessageType.Unknown, lineEvent.Message.MessageType);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Invalid)]
        public async Task GetEvents_InvalidRequest_MessageIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.IsNull(lineEvent.Message);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Messages.Audio)]
        public async Task Group_MessageTypeIsAudio_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Messages.Audio);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Audio, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Sticker);
            Assert.IsNull(lineEvent.Message.Text);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Messages.Image)]
        public async Task Group_MessageTypeIsImage_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Messages.Image);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Image, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Sticker);
            Assert.IsNull(lineEvent.Message.Text);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Messages.Location)]
        public async Task Group_MessageTypeIsLocation_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Messages.Location);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.AreEqual(MessageType.Location, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Text);
            Assert.IsNull(lineEvent.Message.Sticker);

            ILocation location = lineEvent.Message.Location;
            Assert.IsNotNull(location);
            Assert.AreEqual("〒150-0002 東京都渋谷区渋谷２丁目２１−１", location.Address);
            Assert.AreEqual(35.65910807942215m, location.Latitude);
            Assert.AreEqual(139.70372892916203m, location.Longitude);
            Assert.AreEqual("my location", location.Title);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Messages.Sticker)]
        public async Task Group_MessageTypeIsSticker_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Messages.Sticker);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Sticker, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Text);

            ISticker sticker = lineEvent.Message.Sticker;
            Assert.IsNotNull(sticker);
            Assert.AreEqual("1", sticker.PackageId);
            Assert.AreEqual("2", sticker.StickerId);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Messages.Text)]
        public async Task Group_MessageTypeIsText_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Messages.Text);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Text, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Sticker);
            Assert.AreEqual("Hello, world", lineEvent.Message.Text);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Messages.Video)]
        public async Task Group_MessageTypeIsVideo_ReturnsMessage()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Messages.Video);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.User, source.SourceType);
            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.IsNotNull(lineEvent.Message);
            Assert.AreEqual("325708", lineEvent.Message.Id);
            Assert.IsNull(lineEvent.Message.Location);
            Assert.AreEqual(MessageType.Video, lineEvent.Message.MessageType);
            Assert.AreEqual("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.IsNull(lineEvent.Message.Sticker);
            Assert.IsNull(lineEvent.Message.Text);
        }
    }
}
