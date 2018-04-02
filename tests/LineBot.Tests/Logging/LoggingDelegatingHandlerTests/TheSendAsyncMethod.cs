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
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests.Logging
{
    public partial class LoggingDelegatingHandlerTests
    {
        [TestClass]
        public class TheSendAsyncMethod
        {
            [TestMethod]
            public async Task ShouldLogApiCall()
            {
                var logger = new TestLogger();
                var innerHandler = new TestHttpMessageHandler(new byte[] { });

                var loggingDelegatingHandler = new LoggingDelegatingHandler(logger, innerHandler);

                var httpClient = new HttpClient(loggingDelegatingHandler);

                await httpClient.PostAsync("https://foo.bar", new StringContent("Test"));

                Assert.AreEqual(new Uri("https://foo.bar/"), logger.LogApiCallUri);

                var content = await logger.LogApiCallHttpContent.ReadAsStringAsync();
                Assert.AreEqual("Test", content);
            }
        }
    }
}
