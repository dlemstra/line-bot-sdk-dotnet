// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var column = new CarouselColumn
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "Foo",
                    Text = "Test"
                };

                var serialized = JsonSerializer.SerializeObject(column);
                Assert.AreEqual(@"{""thumbnailImageUrl"":""https://foo.bar"",""title"":""Foo"",""text"":""Test""}", serialized);
            }
        }
    }
}
