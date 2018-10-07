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
            [TestMethod]
            public async Task ThrowsExceptionWhenRichMenuIdIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();

                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                {
                    await bot.SetDefaultRichMenu(null);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenRichMenuIdIsEmpty()
            {
                ILineBot bot = TestConfiguration.CreateBot();

                await ExceptionAssert.ThrowsArgumentException("richMenuId", "Value cannot be empty.", async () =>
                {
                    await bot.SetDefaultRichMenu(string.Empty);
                });
            }

            [TestMethod]
            public async Task SetDefaultRichMenu_CorrectInput_CallsApi()
            {
                byte[] input = new byte[0];
                var sampleId = Guid.NewGuid().ToString();
                var postedData = "{}";

                TestHttpClient httpClient = TestHttpClient.ThatReturnsData(input);

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                await bot.SetDefaultRichMenu(sampleId);

                Assert.AreEqual(HttpMethod.Post, httpClient.RequestMethod);
                Assert.AreEqual($"/user/all/richmenu/{sampleId}", httpClient.RequestPath);
                Assert.AreEqual(postedData, httpClient.PostedData);
            }

            [TestMethod]
            public async Task SetDefaultRichMenu_CorrectInput_UnsuccessfulApiCall()
            {
                var sampleId = Guid.NewGuid().ToString();

                TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();
                ILineBot bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                {
                    await bot.SetDefaultRichMenu(sampleId);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenRichMenuResponseIdIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();

                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                {
                    await bot.SetDefaultMenu(null);
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenRichMenusIdIsNull()
            {
                ILineBot bot = TestConfiguration.CreateBot();

                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                {
                    await bot.SetDefaultMenu(new RichMenuResponse());
                });
            }

            [TestMethod]
            public async Task ThrowsExceptionWhenRichMenusIdIsEmpty()
            {
                ILineBot bot = TestConfiguration.CreateBot();
                var richMenuResponse = new RichMenuResponse()
                {
                    Id = string.Empty
                };

                await ExceptionAssert.ThrowsArgumentException("richMenuId", "Value cannot be empty.", async () =>
                {
                    await bot.SetDefaultMenu(richMenuResponse);
                });
            }

            [TestMethod]
            public async Task SetDefaultMenu_CorrectInput_CallsApi()
            {
                byte[] input = new byte[0];
                var sampleId = Guid.NewGuid().ToString();
                var postedData = "{}";
                var richMenuResponse = new RichMenuResponse()
                {
                    Id = sampleId
                };

                TestHttpClient httpClient = TestHttpClient.ThatReturnsData(input);

                ILineBot bot = TestConfiguration.CreateBot(httpClient);
                await bot.SetDefaultMenu(richMenuResponse);

                Assert.AreEqual(HttpMethod.Post, httpClient.RequestMethod);
                Assert.AreEqual($"/user/all/richmenu/{sampleId}", httpClient.RequestPath);
                Assert.AreEqual(postedData, httpClient.PostedData);
            }

            [TestMethod]
            public async Task SetDefaultMenu_CorrectInput_UnsuccessfulApiCall()
            {
                var sampleId = Guid.NewGuid().ToString();
                var richMenuResponse = new RichMenuResponse()
                {
                    Id = sampleId
                };

                TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();
                ILineBot bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                {
                    await bot.SetDefaultMenu(richMenuResponse);
                });
            }
        }
    }
}
