// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class MessageTests
    {
        [Fact]
        public async Task GetEvents_RequestWithoutMessage_MessageIsNull()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.WithoutMessage);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.NotNull(events);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Equal(LineEventType.Message, lineEvent.EventType);
            Assert.Null(lineEvent.Message);
        }

        [Fact]
        public async Task GetEvents_InvalidMessageType_MessageTypeIsUnknown()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Messages.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.NotNull(lineEvent.Message);
            Assert.Equal(MessageType.Unknown, lineEvent.Message.MessageType);
        }

        [Fact]
        public async Task GetEvents_InvalidRequest_MessageIsNull()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Invalid);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            Assert.Null(lineEvent.Message);
        }

        [Fact]
        public async Task Group_MessageTypeIsAudio_ReturnsMessage()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Messages.Audio);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.NotNull(lineEvent.Message);
            Assert.Equal("325708", lineEvent.Message.Id);
            Assert.Null(lineEvent.Message.Location);
            Assert.Equal(MessageType.Audio, lineEvent.Message.MessageType);
            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.Null(lineEvent.Message.Sticker);
            Assert.Null(lineEvent.Message.Text);
        }

        [Fact]
        public async Task Group_MessageTypeIsImage_ReturnsMessage()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Messages.Image);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.NotNull(lineEvent.Message);
            Assert.Equal("325708", lineEvent.Message.Id);
            Assert.Null(lineEvent.Message.Location);
            Assert.Equal(MessageType.Image, lineEvent.Message.MessageType);
            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.Null(lineEvent.Message.Sticker);
            Assert.Null(lineEvent.Message.Text);
        }

        [Fact]
        public async Task Group_MessageTypeIsLocation_ReturnsMessage()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Messages.Location);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.NotNull(lineEvent.Message);
            Assert.Equal("325708", lineEvent.Message.Id);
            Assert.Equal(MessageType.Location, lineEvent.Message.MessageType);
            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.Null(lineEvent.Message.Text);
            Assert.Null(lineEvent.Message.Sticker);

            var location = lineEvent.Message.Location;
            Assert.NotNull(location);
            Assert.Equal("〒150-0002 東京都渋谷区渋谷２丁目２１−１", location.Address);
            Assert.Equal(35.65910807942215m, location.Latitude);
            Assert.Equal(139.70372892916203m, location.Longitude);
            Assert.Equal("my location", location.Title);
        }

        [Fact]
        public async Task Group_MessageTypeIsSticker_ReturnsMessage()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Messages.Sticker);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.NotNull(lineEvent.Message);
            Assert.Equal("325708", lineEvent.Message.Id);
            Assert.Null(lineEvent.Message.Location);
            Assert.Equal(MessageType.Sticker, lineEvent.Message.MessageType);
            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.Null(lineEvent.Message.Text);

            var sticker = lineEvent.Message.Sticker;
            Assert.NotNull(sticker);
            Assert.Equal("1", sticker.PackageId);
            Assert.Equal("2", sticker.StickerId);
        }

        [Fact]
        public async Task Group_MessageTypeIsText_ReturnsMessage()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Messages.Text);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.NotNull(lineEvent.Message);
            Assert.Equal("325708", lineEvent.Message.Id);
            Assert.Null(lineEvent.Message.Location);
            Assert.Equal(MessageType.Text, lineEvent.Message.MessageType);
            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.Null(lineEvent.Message.Sticker);
            Assert.Equal("Hello, world", lineEvent.Message.Text);
        }

        [Fact]
        public async Task Group_MessageTypeIsVideo_ReturnsMessage()
        {
            var bot = TestConfiguration.CreateBot();
            var request = new TestHttpRequest(JsonDocuments.Events.Messages.Video);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.Single(events);

            var lineEvent = events.First();

            var source = lineEvent.Source;
            Assert.NotNull(source);
            Assert.Equal(EventSourceType.User, source.SourceType);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);

            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.ReplyToken);

            Assert.NotNull(lineEvent.Message);
            Assert.Equal("325708", lineEvent.Message.Id);
            Assert.Null(lineEvent.Message.Location);
            Assert.Equal(MessageType.Video, lineEvent.Message.MessageType);
            Assert.Equal("nHuyWiB7yP5Zw52FIkcQobQuGDXCTA", lineEvent.Message.ReplyToken);
            Assert.Null(lineEvent.Message.Sticker);
            Assert.Null(lineEvent.Message.Text);
        }
    }
}
