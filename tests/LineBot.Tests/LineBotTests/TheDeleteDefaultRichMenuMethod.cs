// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        [TestClass]
        public class TheDeleteDefaultRichMenuMethod
        {
            [TestMethod]
            public async Task ShouldCallTheCorrectApi()
            {
                var httpClient = TestHttpClient.Create();
                var bot = TestConfiguration.CreateBot(httpClient);
                await bot.DeleteDefaultRichMenu();

                Assert.AreEqual(HttpMethod.Delete, httpClient.RequestMethod);
                Assert.AreEqual($"/user/all/richmenu", httpClient.RequestPath);
            }

            [TestMethod]
            public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
            {
                var httpClient = TestHttpClient.ThatReturnsAnError();
                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                {
                    await bot.DeleteDefaultRichMenu();
                });
            }

            [TestMethod]
            public async Task ShouldReturnTheInstance()
            {
                var httpClient = TestHttpClient.Create();
                var bot = TestConfiguration.CreateBot(httpClient);

                var result = await bot.DeleteDefaultRichMenu();
                Assert.AreSame(bot, result);
            }
        }
    }
}
