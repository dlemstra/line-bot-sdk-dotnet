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
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class RichMenuTests
    {
        [TestMethod]
        public async Task CreateRichMenu_RichMenuIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenu", async () =>
            {
                await bot.CreateRichMenu(null);
            });
        }

        [TestMethod]
        public async Task CreateRichMenu_CorrectInput_CallsApi()
        {
            var richMenu = new RichMenu()
            {
                ChatBarText = "testChatBarTxt",
                Name = "testName",
                RichMenuAreas = new RichMenuArea[]
                {
                    new RichMenuArea
                    {
                        Action = new UriAction { Label = "testLabel", Url = new Uri("http://www.google.com") },
                        RichMenuBounds = new RichMenuBounds()
                        {
                            Height = 100,
                            Width = 100,
                            X = 0,
                            Y = 0
                        }
                    },
                    new RichMenuArea
                    {
                        Action = new UriAction {Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                        RichMenuBounds = new RichMenuBounds()
                        {
                            Height = 200,
                            Width = 200,
                            X = 100,
                            Y = 0
                        }
                    },
                },
                RichMenuSize = new RichMenuSize() { Height = 1686, Width = 2500 },
                Selected = false
            };

            var richMenuIdJson = @"{""richMenuId"": ""richmenu-801b2cd26b2f13587329ed501d279d27""}";
            TestHttpClient httpClient = TestHttpClient.ThatReturnsJsonString(richMenuIdJson);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            var result = await bot.CreateRichMenu(richMenu);

            Assert.AreEqual("/richmenu", httpClient.RequestPath);

            string postedData = @"{""size"":{""width"":2500,""height"":1686},""selected"":false,""name"":""testName"",""chatBarText"":""testChatBarTxt"",""areas"":[{""bounds"":{""x"":0,""y"":0,""width"":100,""height"":100},""action"":{""type"":""uri"",""label"":""testLabel"",""uri"":""http://www.google.com""}},{""bounds"":{""x"":100,""y"":0,""width"":200,""height"":200},""action"":{""type"":""uri"",""label"":""testLabel2"",""uri"":""http://www.bing.com""}}]}";
            Assert.AreEqual(postedData, httpClient.PostedData);

            Assert.AreEqual(result, "richmenu-801b2cd26b2f13587329ed501d279d27");
        }

    }
}