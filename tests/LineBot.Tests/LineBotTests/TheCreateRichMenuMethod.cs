// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheCreateRichMenuMethod
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenRichMenuIsNull()
            {
                var bot = TestConfiguration.CreateBot();

                await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenu", async () =>
                {
                    await bot.CreateRichMenu(null);
                });
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenRichMenuIsInvalid()
            {
                var richMenu = new RichMenu();

                var bot = TestConfiguration.CreateBot();

                await ExceptionAssert.ThrowsAsync<InvalidOperationException>("The areas cannot be null.", async () =>
                {
                    await bot.CreateRichMenu(richMenu);
                });
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenResponseIsError()
            {
                var richMenu = CreateRichMenu();

                var httpClient = TestHttpClient.ThatReturnsAnError();

                var bot = TestConfiguration.CreateBot(httpClient);

                await ExceptionAssert.ThrowsUnknownError(async () =>
                {
                    await bot.CreateRichMenu(richMenu);
                });
            }

            [Fact]
            public async Task ShouldReturnNullWhenResponseContainsWhitespace()
            {
                var richMenu = CreateRichMenu();

                var httpClient = TestHttpClient.Create(JsonDocuments.Whitespace);
                var bot = TestConfiguration.CreateBot(httpClient);
                var richMenuId = await bot.CreateRichMenu(richMenu);

                Assert.Null(richMenuId);
            }

            [Fact]
            public async Task ShouldCreateRichMenu()
            {
                var richMenu = CreateRichMenu();

                var richMenuIdJson = @"{""richMenuId"": ""richmenu-801b2cd26b2f13587329ed501d279d27""}";
                var httpClient = TestHttpClient.ThatReturnsData(Encoding.ASCII.GetBytes(richMenuIdJson));

                var bot = TestConfiguration.CreateBot(httpClient);
                var result = await bot.CreateRichMenu(richMenu);

                Assert.Equal("/richmenu", httpClient.RequestPath);

                var postedData =
                    @"{""areas"":[{""action"":{""type"":""uri"",""label"":""testLabel"",""uri"":""http://www.google.com""},""bounds"":{""x"":11,""y"":12,""width"":110,""height"":120}},{""action"":{""type"":""uri"",""label"":""testLabel2"",""uri"":""http://www.bing.com""},""bounds"":{""x"":21,""y"":22,""width"":210,""height"":220}}],""chatBarText"":""testChatBarTxt"",""name"":""testName"",""selected"":false,""size"":{""width"":2500,""height"":1686}}";
                Assert.Equal(postedData, httpClient.PostedData);

                Assert.Equal("richmenu-801b2cd26b2f13587329ed501d279d27", result);
            }

            private static RichMenu CreateRichMenu()
            {
                return new RichMenu()
                {
                    Size = new RichMenuSize(1686),

                    Selected = false,

                    Name = "testName",

                    ChatBarText = "testChatBarTxt",

                    Areas = new[]
                    {
                        new RichMenuArea()
                        {
                            Action = new UriAction { Label = "testLabel", Url = new Uri("http://www.google.com") },
                            Bounds = new RichMenuBounds
                            {
                                Width = 110,
                                Height = 120,
                                X = 11,
                                Y = 12
                            }
                        },
                        new RichMenuArea
                        {
                            Action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                            Bounds = new RichMenuBounds
                            {
                                Width = 210,
                                Height = 220,
                                X = 21,
                                Y = 22
                            }
                        }
                    }
                };
            }
        }
    }
}
