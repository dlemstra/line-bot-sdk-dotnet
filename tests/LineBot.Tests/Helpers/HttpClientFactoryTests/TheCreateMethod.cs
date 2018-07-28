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

using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class HttpClientFactoryTests
    {
        [TestClass]
        public class TheCreateMethod
        {
            [TestMethod]
            public void ShouldReturnNewInstance()
            {
                LineConfiguration configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnNewInstance)
                };

                HttpClient clientA = HttpClientFactory.Create(configuration, null);
                HttpClient clientB = HttpClientFactory.Create(configuration, null);

                Assert.AreNotEqual(clientA, clientB);
            }

            [TestMethod]
            public void ShouldReturnInitializedHttpClient()
            {
                LineConfiguration configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnInitializedHttpClient)
                };

                HttpClient client = HttpClientFactory.Create(configuration, null);

                Assert.AreEqual(new Uri("https://api.line.me/v2/bot/"), client.BaseAddress);
                string authorization = client.DefaultRequestHeaders.GetValues("Authorization").First();
                Assert.AreEqual("Bearer " + nameof(ShouldReturnInitializedHttpClient), authorization);
            }

            [TestMethod]
            public void ShouldSetInnerHandlerToLineBotLogger()
            {
                LineConfiguration configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnInitializedHttpClient)
                };

                HttpClient client = HttpClientFactory.Create(configuration, null);

                FieldInfo field = client.GetType().BaseType.GetRuntimeFields().Where(f => f.Name == "_handler").First();
                LoggingDelegatingHandler logger = field.GetValue(client) as LoggingDelegatingHandler;
                Assert.IsNotNull(logger);
                Assert.IsInstanceOfType(logger.InnerHandler, typeof(HttpClientHandler));
            }
        }
    }
}
