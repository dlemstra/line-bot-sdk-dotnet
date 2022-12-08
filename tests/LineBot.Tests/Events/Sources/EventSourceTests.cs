// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.IO;
using Newtonsoft.Json;
using Xunit;

namespace Line.Tests
{
    public class EventSourceTests
    {
        [Fact]
        public void Deserialize_EventSourceTypeIsInvalid_SourceTypeIsUnknown()
        {
            var source = CreateEventSource(JsonDocuments.Events.Sources.Invalid);
            Assert.Equal(EventSourceType.Unkown, source.SourceType);
            Assert.Null(source.Group);
            Assert.Null(source.Room);
            Assert.Null(source.User);
        }

        [Fact]
        public void Group_EventSourceTypeIsGroup_ReturnsGroup()
        {
            var source = CreateEventSource(JsonDocuments.Events.Sources.Group);
            Assert.Equal(EventSourceType.Group, source.SourceType);

            Assert.Equal("cxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", source.Group.Id);
            Assert.Null(source.Room);
            Assert.Null(source.User);
        }

        [Fact]
        public void Group_EventSourceTypeIsGroupAndUserIdIsSet_ReturnsGroupAndUser()
        {
            var source = CreateEventSource(JsonDocuments.Events.Sources.GroupWithUser);
            Assert.Equal(EventSourceType.Group, source.SourceType);

            Assert.Equal("cxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", source.Group.Id);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);
            Assert.Null(source.Room);
        }

        [Fact]
        public void Room_EventSourceTypeIsRoom_ReturnsRoom()
        {
            var source = CreateEventSource(JsonDocuments.Events.Sources.Room);
            Assert.Equal(EventSourceType.Room, source.SourceType);

            Assert.Equal("cyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy", source.Room.Id);
            Assert.Null(source.Group);
            Assert.Null(source.User);
        }

        [Fact]
        public void Room_EventSourceTypeIsRoomAndUserIdIsSet_ReturnsRoomAndUser()
        {
            var source = CreateEventSource(JsonDocuments.Events.Sources.RoomWithUser);
            Assert.Equal(EventSourceType.Room, source.SourceType);

            Assert.Equal("cyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy", source.Room.Id);
            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);
            Assert.Null(source.Group);
        }

        [Fact]
        public void User_EventSourceTypeIsUser_ReturnsUser()
        {
            var source = CreateEventSource(JsonDocuments.Events.Sources.User);
            Assert.Equal(EventSourceType.User, source.SourceType);

            Assert.Equal("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);
            Assert.Null(source.Group);
            Assert.Null(source.Room);
        }

        private IEventSource CreateEventSource(string documentName)
            => JsonConvert.DeserializeObject<EventSource>(File.ReadAllText(documentName));
    }
}
