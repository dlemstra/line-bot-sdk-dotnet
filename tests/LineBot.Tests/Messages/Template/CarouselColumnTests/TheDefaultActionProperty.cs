// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        public class TheDefaultActionProperty
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var column = new CarouselColumn
                {
                    DefaultAction = null
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsInvalid()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    column.DefaultAction = new TestAction();
                });
            }
        }
    }
}
