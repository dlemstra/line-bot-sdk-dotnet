// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImageCarouselColumnTests
    {
        public class TheActionProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var column = new ImageCarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    column.Action = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenTemplateActionTypeIsInvalid()
            {
                var column = new ImageCarouselColumn();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    column.Action = new TestAction();
                });
            }
        }
    }
}
