// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ISendMessageExtensionsTests
    {
        public class TheValidateAndConvertMethod : ISendMessageExtensionsTests
        {
            [Fact]
            public void ShouldThrowExceptionWhenCalledWithMoreThanFiveMessages()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of messages is 5.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[6]);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrarHasNullValue()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The message should not be null.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[1] { null });
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenMessageTypeIsInvalid()
            {
                ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[1] { new InvalidMessage() });
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenMessageIsInvalid()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[1] { new TextMessage() });
                });
            }
        }
    }
}
