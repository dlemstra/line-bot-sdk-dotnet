// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class UriActionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var action = new UriAction
                {
                    Label = "Foo",
                    Url = new Uri("http://foo.bar")
                };

                var serialized = JsonSerializer.SerializeObject(action);
                Assert.Equal(@"{""type"":""uri"",""label"":""Foo"",""uri"":""http://foo.bar""}", serialized);
            }
        }
    }
}
