// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class AudioMessageTests
    {
        public class TheDurationProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsZero()
            {
                var message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
                {
                    message.Duration = 0;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMinusOne()
            {
                var message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
                {
                    message.Duration = -1;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan59999()
            {
                var message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration cannot be longer than 1 minute.", () =>
                {
                    message.Duration = 60000;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenUrlIs59999Milliseconds()
            {
                var value = 59999;

                var message = new AudioMessage()
                {
                    Duration = value
                };

                Assert.Equal(value, message.Duration);
            }
        }
    }
}
