// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheSetUserRichMenuMethod
        {
            public class WithUserIdAndRichMenuId
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
                    {
                        await bot.SetUserRichMenu((string)null, "test");
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
                    {
                        await bot.SetUserRichMenu(string.Empty, "test");
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetUserRichMenu("test", (string)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetUserRichMenu("test", string.Empty);
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var userId = Guid.NewGuid().ToString();
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.SetUserRichMenu(userId, richMenuId);

                    Assert.Equal(HttpMethod.Post, httpClient.RequestMethod);
                    Assert.Equal($"/user/{userId}/richmenu/{richMenuId}", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var userId = Guid.NewGuid().ToString();
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.SetUserRichMenu(userId, richMenuId);
                    });
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.SetUserRichMenu("test", "test");
                    Assert.Same(bot, result);
                }
            }

            public class WithUserAndRichMenuId
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenUserIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
                    {
                        await bot.SetUserRichMenu((IUser)null, "test");
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsNull()
                {
                    var user = new TestUser()
                    {
                        Id = null
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
                    {
                        await bot.SetUserRichMenu(user, "test");
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsEmpty()
                {
                    var user = new TestUser()
                    {
                        Id = string.Empty
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
                    {
                        await bot.SetUserRichMenu(user, "test");
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var user = new TestUser();

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetUserRichMenu(user, (string)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var user = new TestUser();

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetUserRichMenu(user, string.Empty);
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var user = new TestUser();

                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.SetUserRichMenu(user, richMenuId);

                    Assert.Equal(HttpMethod.Post, httpClient.RequestMethod);
                    Assert.Equal($"/user/{user.Id}/richmenu/{richMenuId}", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var user = new TestUser();
                    var richMenuId = Guid.NewGuid().ToString();

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.SetUserRichMenu(user, richMenuId);
                    });
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var user = new TestUser();

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.SetUserRichMenu(user, "test");
                    Assert.Same(bot, result);
                }
            }

            public class WithhUserIdAndRichMenuResponse
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsNull()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
                    {
                        await bot.SetUserRichMenu((string)null, richMenuResponse);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsEmpty()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
                    {
                        await bot.SetUserRichMenu(string.Empty, "test");
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIsNull()
                {
                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenu", async () =>
                    {
                        await bot.SetUserRichMenu("test", (IRichMenuResponse)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var richMenu = new RichMenuResponse()
                    {
                        Id = null
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetUserRichMenu("test", richMenu);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var richMenu = new RichMenuResponse()
                    {
                        Id = string.Empty
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetUserRichMenu("test", richMenu);
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var userId = Guid.NewGuid().ToString();
                    var richMenu = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.SetUserRichMenu(userId, richMenu);

                    Assert.Equal(HttpMethod.Post, httpClient.RequestMethod);
                    Assert.Equal($"/user/{userId}/richmenu/{richMenu.Id}", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var userId = Guid.NewGuid().ToString();
                    var richMenu = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.SetUserRichMenu(userId, richMenu);
                    });
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var richMenu = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.SetUserRichMenu("test", richMenu);
                    Assert.Same(bot, result);
                }
            }

            public class WithUserAndRichMenuResponse
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenUserIsNull()
                {
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("user", async () =>
                    {
                        await bot.SetUserRichMenu((IUser)null, richMenuResponse);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsNull()
                {
                    var user = new TestUser()
                    {
                        Id = null
                    };
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("userId", async () =>
                    {
                        await bot.SetUserRichMenu(user, richMenuResponse);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenUserIdIsEmpty()
                {
                    var user = new TestUser()
                    {
                        Id = string.Empty
                    };
                    var richMenuResponse = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("userId", async () =>
                    {
                        await bot.SetUserRichMenu(user, richMenuResponse);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIsNull()
                {
                    var user = new TestUser();

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenu", async () =>
                    {
                        await bot.SetUserRichMenu(user, (IRichMenuResponse)null);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsNull()
                {
                    var user = new TestUser();
                    var richMenu = new RichMenuResponse()
                    {
                        Id = null
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentNullExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetUserRichMenu(user, richMenu);
                    });
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenRichMenuIdIsEmpty()
                {
                    var user = new TestUser();
                    var richMenu = new RichMenuResponse()
                    {
                        Id = string.Empty
                    };

                    var bot = TestConfiguration.CreateBot();

                    await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("richMenuId", async () =>
                    {
                        await bot.SetUserRichMenu(user, richMenu);
                    });
                }

                [Fact]
                public async Task ShouldCallTheCorrectApi()
                {
                    var user = new TestUser();
                    var richMenu = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);
                    await bot.SetUserRichMenu(user, richMenu);

                    Assert.Equal(HttpMethod.Post, httpClient.RequestMethod);
                    Assert.Equal($"/user/{user.Id}/richmenu/{richMenu.Id}", httpClient.RequestPath);
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenApiCallIsUnsuccessful()
                {
                    var user = new TestUser();
                    var richMenu = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.ThatReturnsAnError();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    await ExceptionAssert.ThrowsAsync<LineBotException>(async () =>
                    {
                        await bot.SetUserRichMenu(user, richMenu);
                    });
                }

                [Fact]
                public async Task ShouldReturnTheInstance()
                {
                    var user = new TestUser();
                    var richMenu = new RichMenuResponse()
                    {
                        Id = Guid.NewGuid().ToString()
                    };

                    var httpClient = TestHttpClient.Create();
                    var bot = TestConfiguration.CreateBot(httpClient);

                    var result = await bot.SetUserRichMenu(user, richMenu);
                    Assert.Same(bot, result);
                }
            }
        }
    }
}
