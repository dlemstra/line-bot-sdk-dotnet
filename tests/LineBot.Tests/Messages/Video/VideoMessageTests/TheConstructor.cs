// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class VideoMessageTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var message = new VideoMessage()
                {
                    Url = new Uri("https://foo.url"),
                    PreviewUrl = new Uri("https://foo.previewUrl"),
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.Equal(@"{""type"":""video"",""originalContentUrl"":""https://foo.url"",""previewImageUrl"":""https://foo.previewUrl""}", serialized);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var message = new VideoMessage(new Uri("https://foo.url"), new Uri("https://foo.previewUrl"));

                Assert.Equal("https://foo.url/", message.Url.ToString());
                Assert.Equal("https://foo.previewurl/", message.PreviewUrl.ToString());
            }

            [Fact]
            public void ShouldConvertStringUrl()
            {
                var message = new VideoMessage("https://foo.url", "https://foo.previewUrl");

                Assert.Equal("https://foo.url/", message.Url.ToString());
                Assert.Equal("https://foo.previewurl/", message.PreviewUrl.ToString());
            }
        }
    }
}
