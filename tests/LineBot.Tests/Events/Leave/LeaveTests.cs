// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class LeaveTests
    {
        [TestMethod]
        [DeploymentItem(JsonDocuments.Events.Leave)]
        public async Task GetEvents_ValidRequest_IsLeaveEvent()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(JsonDocuments.Events.Leave);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count());

            ILineEvent lineEvent = events.First();

            Assert.AreEqual(LineEventType.Leave, lineEvent.EventType);

            IEventSource source = lineEvent.Source;
            Assert.IsNotNull(source);
            Assert.AreEqual(EventSourceType.Group, source.SourceType);
            Assert.AreEqual("cxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", source.Group.Id);
        }
    }
}
