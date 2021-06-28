// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class StickerMessageTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenPackageIdIsNull()
            {
                ISendMessage message = new StickerMessage()
                {
                    StickerId = "StickerId"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The package id cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStickerIdIsNull()
            {
                ISendMessage message = new StickerMessage()
                {
                    PackageId = "PackageId"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The sticker id cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ISendMessage message = new StickerMessage()
                {
                    StickerId = "StickerId",
                    PackageId = "PackageId"
                };

                message.Validate();
            }
        }
    }
}
