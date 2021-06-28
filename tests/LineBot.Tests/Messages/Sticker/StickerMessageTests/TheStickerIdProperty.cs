// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class StickerMessageTests
    {
        [TestClass]
        public class TheStickerIdProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new StickerMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The sticker id cannot be null or whitespace.", () =>
                {
                    message.StickerId = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new StickerMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The sticker id cannot be null or whitespace.", () =>
                {
                    message.StickerId = string.Empty;
                });
            }
        }
    }
}
