// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
            [DeploymentItem(JsonDocuments.RichMenu.DefaultRichMenuId)]
            public async Task ReturnsIdWhenResponseIsCorrect()
            {
                TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.DefaultRichMenuId);
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
