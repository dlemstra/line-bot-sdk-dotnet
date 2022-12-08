// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class AudioMessageTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenUrlIsNull()
            {
                ISendMessage message = new AudioMessage()
                {
                    Duration = 10000
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenDurationIsZero()
            {
                ISendMessage message = new AudioMessage()
                {
                    Url = new Uri("https://foo.url")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ISendMessage message = new AudioMessage()
                {
                    Duration = 10000,
                    Url = new Uri("https://foo.url")
                };

                message.Validate();
            }
        }
    }
}
