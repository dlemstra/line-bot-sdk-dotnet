// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapActionTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenAreaIsNull()
            {
                var action = new InvalidAction();

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            private class InvalidAction : ImagemapAction
            {
                public InvalidAction()
                    : base(ImagemapActionType.Message)
                {
                }
            }
        }
    }
}
