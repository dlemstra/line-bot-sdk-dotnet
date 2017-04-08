// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    [TestClass]
    public class EventSourceTests
    {
        private const string GroupJson = "Events\\Sources\\Group.json";
        private const string InvalidJson = "Events\\Sources\\Invalid.json";
        private const string RoomJson = "Events\\Sources\\Room.json";
        private const string UserJson = "Events\\Sources\\User.json";

        [TestMethod]
        [DeploymentItem(InvalidJson)]
        public void Deserialize_EventSourceTypeIsInvalid_SourceTypeIsUnknown()
        {
            IEventSource source = CreateInvalid();
            Assert.AreEqual(EventSourceType.Unkown, source.SourceType);
        }

        [TestMethod]
        [DeploymentItem(GroupJson)]
        public void GetGroup_EventSourceTypeIsNotGroup_ThrowsException()
        {
            IEventSource source = CreateUser();

            ExceptionAssert.Throws<InvalidOperationException>("SourceType should be Group.", () =>
            {
                IGroup group = source.Group;
            });
        }

        [TestMethod]
        [DeploymentItem(GroupJson)]
        public void GetGroup_EventSourceTypeIsGroup_ReturnsGroup()
        {
            IEventSource source = CreateGroup();
            Assert.AreEqual(EventSourceType.Group, source.SourceType);

            Assert.AreEqual("cxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", source.Group.Id);
        }

        [TestMethod]
        [DeploymentItem(UserJson)]
        public void GetRoom_EventSourceTypeIsNotRoom_ThrowsException()
        {
            IEventSource source = CreateUser();

            ExceptionAssert.Throws<InvalidOperationException>("SourceType should be Room.", () =>
            {
                IRoom room = source.Room;
            });
        }

        [TestMethod]
        [DeploymentItem(RoomJson)]
        public void GetRoom_EventSourceTypeIsRoom_ReturnsRoom()
        {
            IEventSource source = CreateRoom();
            Assert.AreEqual(EventSourceType.Room, source.SourceType);

            Assert.AreEqual("cyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy", source.Room.Id);
        }

        [TestMethod]
        [DeploymentItem(GroupJson)]
        public void GetUser_EventSourceTypeIsNotUser_ThrowsException()
        {
            IEventSource source = CreateRoom();

            ExceptionAssert.Throws<InvalidOperationException>("SourceType should be User.", () =>
            {
                IUser user = source.User;
            });
        }

        [TestMethod]
        [DeploymentItem(UserJson)]
        public void GetUser_EventSourceTypeIsUser_ReturnsUser()
        {
            IEventSource source = CreateUser();
            Assert.AreEqual(EventSourceType.User, source.SourceType);

            Assert.AreEqual("U206d25c2ea6bd87c17655609a1c37cb8", source.User.Id);
        }

        private IEventSource CreateGroup()
        {
            return JsonConvert.DeserializeObject<EventSource>(File.ReadAllText(GroupJson));
        }

        private IEventSource CreateInvalid()
        {
            return JsonConvert.DeserializeObject<EventSource>(File.ReadAllText(InvalidJson));
        }

        private IEventSource CreateRoom()
        {
            return JsonConvert.DeserializeObject<EventSource>(File.ReadAllText(RoomJson));
        }

        private IEventSource CreateUser()
        {
            return JsonConvert.DeserializeObject<EventSource>(File.ReadAllText(UserJson));
        }
    }
}
