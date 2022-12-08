// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class StickerMessageTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var message = new StickerMessage("CorrectPackage", "CorrectSticker");

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.Equal(@"{""type"":""sticker"",""packageId"":""CorrectPackage"",""stickerId"":""CorrectSticker""}", serialized);
            }
        }
    }
}
