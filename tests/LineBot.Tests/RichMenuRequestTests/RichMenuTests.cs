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
    [TestClass]
    public class RichMenuTests
    {
        [TestMethod]
        public async Task ShouldThrowExceptionWhenRichMenuIsNull()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuRequest", async () =>
            {
                await bot.CreateRichMenuRequest(null);
            });
        }

        [TestMethod]
        public async Task ShouldNotThrowExceptionWhenRichMenuIsGood()
        {
            var richMenu = new TestRichMenuRequest();

            var richMenuIdJson = @"{""richMenuId"": ""richmenu-801b2cd26b2f13587329ed501d279d27""}";
            var httpClient = TestHttpClient.ThatReturnsData(Encoding.ASCII.GetBytes(richMenuIdJson));

            var bot = TestConfiguration.CreateBot(httpClient);
            var result = await bot.CreateRichMenuRequest(richMenu);

            Assert.AreEqual("/richmenu", httpClient.RequestPath);

            string postedData = @"{""size"":{""width"":2500,""height"":1686},""selected"":false,""name"":""testName"",""chatBarText"":""testChatBarTxt"",""areas"":[{""bounds"":{""x"":11,""y"":12,""width"":110,""height"":120},""action"":{""type"":""uri"",""label"":""testLabel"",""uri"":""http://www.google.com""}},{""bounds"":{""x"":21,""y"":22,""width"":210,""height"":220},""action"":{""type"":""uri"",""label"":""testLabel2"",""uri"":""http://www.bing.com""}}]}";
            Assert.AreEqual(postedData, httpClient.PostedData);

            Assert.AreEqual(result, "richmenu-801b2cd26b2f13587329ed501d279d27");
        }
    }
}