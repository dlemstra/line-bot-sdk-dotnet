// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheGetRichMenusMethod
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenResponseIsError()
            {
                var httpClient = TestHttpClient.ThatReturnsAnError();

                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.GetRichMenus();
                });
            }

            [Fact]
            public async Task ShouldReturnEmptyCollectionWhenResponseContainsWhitespace()
            {
                var httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                var bot = TestConfiguration.CreateBot(httpClient);
                var richMenus = await bot.GetRichMenus();

                Assert.Empty(richMenus);
            }

            [Fact]
            public async Task ShouldReturnEmptyCollectionWhenResponseContainsNoRichMenus()
            {
                var httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.EmptyRichMenuResponseCollection);
                var bot = TestConfiguration.CreateBot(httpClient);
                var richMenus = await bot.GetRichMenus();

                Assert.Empty(richMenus);
            }

            [Fact]
            public async Task ShouldCallTheCorrectApi()
            {
                var httpClient = TestHttpClient.Create(JsonDocuments.EmptyObject);
                var bot = TestConfiguration.CreateBot(httpClient);
                var richMenus = await bot.GetRichMenus();

                Assert.Equal(HttpMethod.Get, httpClient.RequestMethod);
                Assert.Equal($"/richmenu/list", httpClient.RequestPath);
            }

            [Fact]
            public async Task ShouldReturnRichMenuResponseCollectionWhenResponseIsCorrect()
            {
                var httpClient = TestHttpClient.Create(JsonDocuments.RichMenu.RichMenuResponseCollection);
                var bot = TestConfiguration.CreateBot(httpClient);
                var richMenus = await bot.GetRichMenus();

                Assert.NotNull(richMenus);
                Assert.Single(richMenus);

                var richMenu = richMenus.FirstOrDefault();
                Assert.NotNull(richMenu);
                Assert.Equal("f22df647-b12f-427c-85c5-8238bee6bb45", richMenu.Id);
                Assert.False(richMenu.Selected);

                var size = richMenu.Size;
                Assert.NotNull(size);
                Assert.Equal(2500, size.Width);
                Assert.Equal(1686, size.Height);

                Assert.NotNull(richMenu.Areas);
                var area = richMenu.Areas.FirstOrDefault();
                Assert.NotNull(area);

                var bounds = area.Bounds;
                Assert.NotNull(bounds);
                Assert.Equal(1, bounds.X);
                Assert.Equal(2, bounds.Y);
                Assert.Equal(2499, bounds.Width);
                Assert.Equal(1684, bounds.Height);

                var action = area.Action;
                Assert.NotNull(action);
                var postbackAction = action as PostbackAction;
                Assert.NotNull(postbackAction);
                Assert.Equal("action=buy&itemid=123", postbackAction.Data);
            }
        }
    }
}
