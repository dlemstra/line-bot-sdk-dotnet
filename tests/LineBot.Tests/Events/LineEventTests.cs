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
    public class LineEventTests
    {
        private const string EmptyObjectJson = "Events/EmptyObject.json";
        private const string NoEventsJson = "Events/NoEvents.json";
        private const string WhitespaceJson = "Events/Whitespace.json";

        [TestMethod]
        [DeploymentItem(EmptyObjectJson)]
        public async Task GetEvents_EmptyObject_ReturnsEmptyEnumerable()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(EmptyObjectJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(0, events.Count());
        }

        [TestMethod]
        [DeploymentItem(NoEventsJson)]
        public async Task GetEvents_NoEvents_ReturnsEmptyEnumerable()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(NoEventsJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(0, events.Count());
        }

        [TestMethod]
        [DeploymentItem(NoEventsJson)]
        public async Task GetEvents_Whitespace_ReturnsEmptyEnumerable()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest(WhitespaceJson);

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(0, events.Count());
        }

        [TestMethod]
        [DeploymentItem(NoEventsJson)]
        public async Task GetEvents_NoData_ReturnsEmptyEnumerable()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            TestHttpRequest request = new TestHttpRequest();

            IEnumerable<ILineEvent> events = await bot.GetEvents(request);
            Assert.IsNotNull(events);
            Assert.AreEqual(0, events.Count());
        }
    }
}
