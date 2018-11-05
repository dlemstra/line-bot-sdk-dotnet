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

using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheGetDefaultRichMenuMethod
        {
            [TestMethod]
            [DeploymentItem(JsonDocuments.DefaultRichMenuId)]
            public async Task ReturnsIdWhenResponseIsCorrect()
            {
                TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.DefaultRichMenuId);
                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                var id = await bot.GetDefaultRichMenu();

                Assert.AreEqual("110FB567-E204-4131-9669-DB828CE65D2F", id);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.EmptyObject)]
            public async Task ReturnsNullIdWhenResponseContainsEmptyObject()
            {
                TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                var id = await bot.GetDefaultRichMenu();

                Assert.IsNull(id);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.Whitespace)]
            public async Task ReturnsNullIdWhenResponseContainsWhitespace()
            {
                TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                var id = await bot.GetDefaultRichMenu();

                Assert.IsNull(id);
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenResponseIsError()
            {
                TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();
                ILineBot bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsAsync<LineBotException>("Unknown error", async () =>
                {
                    await bot.GetDefaultRichMenu();
                });
            }
        }
    }
}
