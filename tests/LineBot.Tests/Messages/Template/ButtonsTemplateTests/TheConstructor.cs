// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var template = new ButtonsTemplate
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "Foo",
                    Text = "Test"
                };

                var serialized = JsonSerializer.SerializeObject(template);
                Assert.Equal(@"{""type"":""buttons"",""thumbnailImageUrl"":""https://foo.bar"",""imageAspectRatio"":""rectangle"",""imageSize"":""cover"",""title"":""Foo"",""text"":""Test""}", serialized);
            }
        }
    }
}
