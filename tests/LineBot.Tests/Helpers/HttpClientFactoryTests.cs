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
using System.Linq;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class HttpClientFactoryTests
    {
        [TestMethod]
        public void Create_SameChannelAccessToken_ReturnsSameInstance()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = nameof(Create_SameChannelAccessToken_ReturnsSameInstance)
            };

            HttpClient clientA = HttpClientFactory.Create(configuration);
            HttpClient clientB = HttpClientFactory.Create(configuration);

            Assert.AreEqual(clientA, clientB);
        }

        [TestMethod]
        public void Create_DifferentChannelAccessToken_ReturnsDifferentInstance()
        {
            LineConfiguration configurationA = new LineConfiguration()
            {
                ChannelAccessToken = $"{nameof(Create_DifferentChannelAccessToken_ReturnsDifferentInstance)}A"
            };

            LineConfiguration configurationB = new LineConfiguration()
            {
                ChannelAccessToken = $"{nameof(Create_DifferentChannelAccessToken_ReturnsDifferentInstance)}B"
            };

            HttpClient clientA = HttpClientFactory.Create(configurationA);
            HttpClient clientB = HttpClientFactory.Create(configurationB);

            Assert.AreNotEqual(clientA, clientB);
        }

        [TestMethod]
        public void Create_InstanceIsInitialized()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = nameof(Create_InstanceIsInitialized)
            };

            HttpClient client = HttpClientFactory.Create(configuration);

            Assert.AreEqual(new Uri("https://api.line.me/v2/bot/"), client.BaseAddress);
            string authorization = client.DefaultRequestHeaders.GetValues("Authorization").First();
            Assert.AreEqual("Bearer " + nameof(Create_InstanceIsInitialized), authorization);
        }
    }
}
