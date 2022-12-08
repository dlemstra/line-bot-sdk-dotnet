﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class StickerMessageTests
    {
        public class ThePackageIdProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new StickerMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The package id cannot be null or whitespace.", () =>
                {
                    message.PackageId = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new StickerMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The package id cannot be null or whitespace.", () =>
                {
                    message.PackageId = string.Empty;
                });
            }
        }
    }
}
