// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImageMessageTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var message = new ImageMessage()
                {
                    Url = new Uri("https://foo.url"),
                    PreviewUrl = new Uri("https://foo.previewUrl"),
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.Equal(@"{""type"":""image"",""originalContentUrl"":""https://foo.url"",""previewImageUrl"":""https://foo.previewUrl""}", serialized);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var message = new ImageMessage(new Uri("https://foo.url"), new Uri("https://foo.previewUrl"));

                Assert.Equal("https://foo.url/", message.Url.ToString());
                Assert.Equal("https://foo.previewurl/", message.PreviewUrl.ToString());
            }

            [Fact]
            public void ShouldConvertStringUrl()
            {
                var message = new ImageMessage("https://foo.url", "https://foo.previewUrl");

                Assert.Equal("https://foo.url/", message.Url.ToString());
                Assert.Equal("https://foo.previewurl/", message.PreviewUrl.ToString());
            }
        }
    }
}
