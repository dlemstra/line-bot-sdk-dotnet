// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    [TestClass]
    public class EventSourceTests
    {
        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Sources.Invalid)]
        public void Deserialize_EventSourceTypeIsInvalid_SourceTypeIsUnknown()
        {
            IEventSource source = CreateEventSource(JsonDocuments.Events.Sources.Invalid);
            Assert.AreEqual(EventSourceType.Unkown, source.SourceType);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Sources.User)]
        public void Group_EventSourceTypeIsNotGroup_ReturnsNull()
        {
            IEventSource source = CreateEventSource(JsonDocuments.Events.Sources.User);
            Assert.IsNull(source.Group);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Sources.Group)]
        public void Group_EventSourceTypeIsGroup_ReturnsGroup()
        {
            IEventSource source = CreateEventSource(JsonDocuments.Events.Sources.Group);
            Assert.AreEqual(EventSourceType.Group, source.SourceType);

            Assert.AreEqual("cxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", source.Group.Id);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Sources.User)]
        public void Room_EventSourceTypeIsNotRoom_ReturnsNull()
        {
            IEventSource source = CreateEventSource(JsonDocuments.Events.Sources.User);
            Assert.IsNull(source.Room);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Sources.Room)]
        public void Room_EventSourceTypeIsRoom_ReturnsRoom()
        {
            IEventSource source = CreateEventSource(JsonDocuments.Events.Sources.Room);
            Assert.AreEqual(EventSourceType.Room, source.SourceType);

            Assert.AreEqual("cyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy", source.Room.Id);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Sources.Room)]
        public void User_EventSourceTypeIsNotUser_ReturnsNull()
        {
            IEventSource source = CreateEventSource(JsonDocuments.Events.Sources.Room);
            Assert.IsNull(source.User);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Sources.User)]
        public void User_EventSourceTypeIsUser_ReturnsUser()
        {
            IEventSource source = CreateEventSource(JsonDocuments.Events.Sources.User);
            Assert.AreEqual(EventSourceType.User, source.SourceType);

            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);
        }

        private IEventSource CreateEventSource(string documentName)
        {
            return JsonConvert.DeserializeObject<EventSource>(File.ReadAllText(documentName));
        }
    }
}
