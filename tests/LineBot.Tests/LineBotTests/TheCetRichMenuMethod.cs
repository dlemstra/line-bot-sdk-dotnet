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
using System.Text;
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
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () => { await bot.GetRichMenu(null); });
            }

            [TestMethod]
            public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () => { await bot.GetRichMenu(string.Empty); });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenRichMenuIdIsNotExist()
            {
                string richMenuId = "richmenu-XXXX";
                var httpClient = TestHttpClient.ThatReturnsData(Encoding.ASCII.GetBytes(@"{""message"":""Not found""}"));
                var bot = TestConfiguration.CreateBot(httpClient);

                ExceptionAssert.Throws<LineBotException>("Can't find the rich menu.", () =>
                {
                    bot.GetRichMenu(richMenuId);
                });
            }

            [TestMethod]
            public async Task ShouldGetRichMenu()
            {
                string richMenuId = "richmenu-e599a14ed7556ba78afd803d69fe15b5";
                var httpClient = TestHttpClient.ThatReturnsData(Encoding.ASCII.GetBytes(@"{""richMenuId"":""richmenu-e599a14ed7556ba78afd803d69fe15b5"",""name"":""testName"",""size"":{""width"":2500,""height"":1686},""chatBarText"":""testChatBarTxt"",""selected"":false,""areas"":[{""bounds"":{""x"":1,""y"":2,""width"":110,""height"":120},""action"":{""label"":""testLabel"",""type"":""uri"",""uri"":""http://www.google.com""}},{""bounds"":{""x"":130,""y"":4,""width"":150,""height"":160},""action"":{""label"":""testLabel2"",""type"":""uri"",""uri"":""http://www.bing.com""}}]}"));

                var bot = TestConfiguration.CreateBot(httpClient);

                var result = await bot.GetRichMenu(richMenuId);

                Assert.AreEqual("/richmenu/" + richMenuId, httpClient.RequestPath);

                Assert.AreEqual(richMenuId, result.RichMenuId);
            }
        }
    }
}