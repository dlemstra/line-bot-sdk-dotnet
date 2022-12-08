// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Xunit;

namespace Line.Tests
{
    public partial class LineBotTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsExceptionWhenConfigurationIsNull()
            {
                ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
                {
                    new LineBot(null);
                });
            }

            [Fact]
            public void ThrowsNotExceptionWhenLoggerIsNull()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = "ChannelSecret",
                };

                new LineBot(configuration, null);
            }

            [Fact]
            public void ThrowsExceptionWhenChannelAccessTokenIsNull()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = null,
                    ChannelSecret = "ChannelSecret",
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelAccessToken cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [Fact]
            public void ThrowsExceptionWhenChannelAccessTokenIsEmpty()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = string.Empty,
                    ChannelSecret = "ChannelSecret",
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelAccessToken cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [Fact]
            public void ThrowsExceptionWhenChannelAccessTokenIsWhitespace()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "  ",
                    ChannelSecret = "ChannelSecret",
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelAccessToken cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [Fact]
            public void ThrowsExceptionWhenChannelSecretTokenIsNull()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = null,
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelSecret cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [Fact]
            public void ThrowsExceptionWhenChannelSecretTokenIsEmpty()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = string.Empty,
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelSecret cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [Fact]
            public void ThrowsExceptionWhenChannelSecretTokenIsWhitespace()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = "  ",
                };

                ExceptionAssert.ThrowsArgumentException("configuration", "The ChannelSecret cannot be null or whitespace.", () =>
                {
                    new LineBot(configuration);
                });
            }

            [Fact]
            public void ShouldSetBaseAddressToApiWhenHttpFactoryIsUsed()
            {
                var configuration = new LineConfiguration()
                {
                    ChannelAccessToken = "ChannelAccessToken",
                    ChannelSecret = "ChannelSecret",
                };

                ILineBot bot = new LineBot(configuration);

                var field = bot.GetType().GetRuntimeFields().Where(f => f.Name == "_client").First();
                var client = (HttpClient)field.GetValue(bot);

                Assert.Equal(new Uri("https://api.line.me/v2/bot/"), client.BaseAddress);
            }
        }
    }
}
