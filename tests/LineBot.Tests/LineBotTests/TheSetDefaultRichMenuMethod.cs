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
        public class TheSetDefaultRichMenuMethod
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
                        await bot.SetDefaultRichMenu(null);
                    });
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentException("richMenuId", "Value cannot be empty.", async () =>
                    {
                        await bot.SetDefaultRichMenu(string.Empty);
                    });
                }

                [TestMethod]
                public async Task ShouldSetDefaultRichMenuWhenCorrectInputCallsApi()
                {
                    var input = new byte[0];
                    var sampleId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsData(input);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.SetDefaultRichMenu(sampleId);

                    Assert.AreEqual(HttpMethod.Post, httpClient.RequestMethod);
                    Assert.AreEqual($"/user/all/richmenu/{sampleId}", httpClient.RequestPath);
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var sampleId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.SetDefaultRichMenu(sampleId);
                    });
                }
            }

            [TestClass]
            public class WithRichMenuResponse
            {
                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenuResponseIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenu", async () =>
                    {
                        await bot.SetDefaultMenu(null);
                    });
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenusIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetDefaultMenu(new RichMenuResponse());
                    });
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenRichMenusIdIsEmpty()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = string.Empty
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentException("richMenuId", "Value cannot be empty.", async () =>
                    {
                        await bot.SetDefaultMenu(richMenuResponse);
                    });
                }

                [TestMethod]
                public async Task ShouldSetDefaultMenuIdWhenCorrectInputCallsApi()
                {
                    var input = new byte[0];
                    var sampleId = Guid.NewGuid().ToString();
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = sampleId
                    };

                    var httpClient = TestHttpClient.ThatReturnsData(input);
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.SetDefaultMenu(richMenuResponse);

                    Assert.AreEqual(HttpMethod.Post, httpClient.RequestMethod);
                    Assert.AreEqual($"/user/all/richmenu/{sampleId}", httpClient.RequestPath);
                }

                [TestMethod]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var sampleId = Guid.NewGuid().ToString();
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = sampleId
                    };

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.SetDefaultMenu(richMenuResponse);
                    });
                }
            }
        }
    }
}
