// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class AudioMessageTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var message = new AudioMessage()
                {
                    Url = new Uri("https://foo.url"),
                    Duration = 10000
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.Equal(@"{""type"":""audio"",""originalContentUrl"":""https://foo.url"",""duration"":10000}", serialized);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var message = new AudioMessage(new Uri("https://foo.url"), 1000);

                Assert.Equal("https://foo.url/", message.Url.ToString());
                Assert.Equal(1000, message.Duration);
            }

            [Fact]
            public void ShouldConvertStringUrl()
            {
                var message = new AudioMessage("https://foo.url", 1000);

                Assert.Equal("https://foo.url/", message.Url.ToString());
                Assert.Equal(1000, message.Duration);
            }
        }
    }
}
