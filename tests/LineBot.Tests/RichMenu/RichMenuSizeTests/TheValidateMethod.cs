// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class RichMenuSizeTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNotSet()
            {
                var richMenuSize = new RichMenuSize();

                ExceptionAssert.Throws<InvalidOperationException>("The height is not set.", () =>
                {
                    richMenuSize.Validate();
                });
            }
        }
    }
}
