// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class StickerMessageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var message = new StickerMessage("CorrectPackage", "CorrectSticker");

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.AreEqual(@"{""type"":""sticker"",""packageId"":""CorrectPackage"",""stickerId"":""CorrectSticker""}", serialized);
            }
        }
    }
}