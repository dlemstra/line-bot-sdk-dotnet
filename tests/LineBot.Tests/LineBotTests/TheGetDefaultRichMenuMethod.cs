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
            private const string DefaultRichMenuIdJson = "LineBotTests/DefaultRichMenuId.json";
            private const string EmptyObjectJson = "EmptyObject.json";
            private const string WhitespaceJson = "Whitespace.json";

            [TestMethod]
            [DeploymentItem(DefaultRichMenuIdJson)]
            public async Task ReturnsIdWhenResponseIsCorrect()
            {
                TestHttpClient httpClient = TestHttpClient.Create(DefaultRichMenuIdJson);
                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                var id = await bot.GetDefaultRichMenu();

                Assert.AreEqual("110FB567-E204-4131-9669-DB828CE65D2F", id);
            }

            [TestMethod]
            [DeploymentItem(EmptyObjectJson)]
            public async Task ReturnsNullIdWhenResponseContainsEmptyObject()
            {
                TestHttpClient httpClient = TestHttpClient.Create(EmptyObjectJson);
                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                var id = await bot.GetDefaultRichMenu();

                Assert.IsNull(id);
            }

            [TestMethod]
            [DeploymentItem(WhitespaceJson)]
            public async Task ReturnsNullIdWhenResponseContainsWhitespace()
            {
                TestHttpClient httpClient = TestHttpClient.Create(WhitespaceJson);
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
