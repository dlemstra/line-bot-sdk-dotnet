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

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheDeleteRichMenuMethod
        {
            [TestClass]
            public class WithRichMenuId
            {
                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.DeleteRichMenu((string)null);
                    });
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.DeleteRichMenu(string.Empty);
                    });
                }

                [TestMethod]
                public async Task ShouldCallTheCorrectApi()
                {
                    var input = new byte[0];
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsData(input);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.DeleteRichMenu(richMenuId);

                    Assert.AreEqual(HttpMethod.Delete, httpClient.RequestMethod);
                    Assert.AreEqual($"/richmenu/{richMenuId}", httpClient.RequestPath);
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.DeleteRichMenu(richMenuId);
                    });
                }
            }

            [TestClass]
            public class WithRichMenuResponse
            {
                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenuResponseIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenu", async () =>
                    {
                        await bot.DeleteRichMenu((IRichMenuResponse)null);
                    });
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.DeleteRichMenu(new RichMenuResponse());
                    });
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = string.Empty
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.DeleteRichMenu(richMenuResponse);
                    });
                }

                [TestMethod]
                public async Task ShouldSetDefaultRichMenuIdWhenCorrectInputCallsApi()
                {
                    var input = new byte[0];
                    var richMenuId = Guid.NewGuid().ToString();
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = richMenuId
                    };

                    var httpClient = TestHttpClient.ThatReturnsData(input);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.DeleteRichMenu(richMenuResponse);

                    Assert.AreEqual(HttpMethod.Delete, httpClient.RequestMethod);
                    Assert.AreEqual($"/richmenu/{richMenuId}", httpClient.RequestPath);
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.DeleteRichMenu(richMenuResponse);
                    });
                }
            }
        }
    }
}
