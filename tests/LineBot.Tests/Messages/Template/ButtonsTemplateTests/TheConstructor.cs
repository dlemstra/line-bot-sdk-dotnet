// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var template = new ButtonsTemplate
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "Foo",
                    Text = "Test"
                };

                string serialized = JsonSerializer.SerializeObject(template);
                Assert.AreEqual(@"{""type"":""buttons"",""thumbnailImageUrl"":""https://foo.bar"",""imageAspectRatio"":""rectangle"",""imageSize"":""cover"",""title"":""Foo"",""text"":""Test""}", serialized);
            }
        }
    }
}
