// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapUriActionTests
    {
        public class TheAreaProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var action = new ImagemapUriAction();

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    action.Area = null;
                });
            }
        }
    }
}
