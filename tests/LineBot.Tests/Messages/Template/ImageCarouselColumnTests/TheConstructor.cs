// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImageCarouselColumnTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var column = new ImageCarouselColumn
                {
                    ImageUrl = new Uri("https://foo.bar"),
                    Action = new UriAction()
                };

                var serialized = JsonSerializer.SerializeObject(column);
                Assert.Equal(@"{""imageUrl"":""https://foo.bar"",""action"":{""type"":""uri""}}", serialized);
            }
        }
    }
}
