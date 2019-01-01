// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheGetRichMenuMethod
        {
            [TestMethod]
            public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
            {
                var bot = TestConfiguration.CreateBot();

                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                {
                    await bot.GetRichMenu(null);
                });
            }

            [TestMethod]
            public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
            {
                var bot = TestConfiguration.CreateBot();

                await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                {
                    await bot.GetRichMenu(string.Empty);
                });
            }

            [TestMethod]
            public async Task ShouldThrowExceptionWhenResponseIsError()
            {
                var httpClient = TestHttpClient.ThatReturnsAnError();

                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.GetRichMenu("test");
                });
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.Whitespace)]
            public async Task ShouldReturnNullWhenResponseContainsWhitespace()
            {
                var httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                var bot = TestConfiguration.CreateBot(httpClient);
                var richMenu = await bot.GetRichMenu("test");

                Assert.IsNull(richMenu);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.EmptyObject)]
            public async Task ShouldCallTheCorrectApi()
            {
                var richMenuId = "f22df647-b12f-427c-85c5-8238bee6bb45";

                var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                var bot = TestConfiguration.CreateBot(httpClient);
                var richMenu = await bot.GetRichMenu(richMenuId);

                Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
                Assert.AreEqual($"/richmenu/{richMenuId}", httpClient.RequestPath);
            }

            [TestMethod]
            [DeploymentItem(JsonDocuments.RichMenu.RichMenuResponse)]
            public async Task ShouldReturnRichMenuResponseWhenResponseIsCorrect()
            {
                var richMenuId = "f22df647-b12f-427c-85c5-8238bee6bb45";

                var httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.RichMenuResponse);
                var bot = TestConfiguration.CreateBot(httpClient);
                var richMenu = await bot.GetRichMenu(richMenuId);

                Assert.IsNotNull(richMenu);
                Assert.AreEqual("f22df647-b12f-427c-85c5-8238bee6bb45", richMenu.Id);
                Assert.IsFalse(richMenu.Selected);

                var size = richMenu.Size;
                Assert.IsNotNull(size);
                Assert.AreEqual(2500, size.Width);
                Assert.AreEqual(1686, size.Height);

                Assert.IsNotNull(richMenu.Areas);
                var area = richMenu.Areas.FirstOrDefault();
                Assert.IsNotNull(area);

                var bounds = area.Bounds;
                Assert.IsNotNull(bounds);
                Assert.AreEqual(1, bounds.X);
                Assert.AreEqual(2, bounds.Y);
                Assert.AreEqual(2499, bounds.Width);
                Assert.AreEqual(1684, bounds.Height);

                var action = area.Action;
                Assert.IsNotNull(action);
                var postbackAction = action as PostbackAction;
                Assert.IsNotNull(postbackAction);
                Assert.AreEqual("action=buy&itemid=123", postbackAction.Data);
            }
        }
    }
}
