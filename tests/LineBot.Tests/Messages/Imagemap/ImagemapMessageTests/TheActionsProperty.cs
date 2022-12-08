// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapMessageTests
    {
        public class TheActionsProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    message.Actions = null;
                });
            }
        }
    }
}
