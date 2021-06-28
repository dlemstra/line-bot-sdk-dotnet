// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ISendMessageExtensionsTests
    {
        [TestClass]
        public class TheValidateAndConvertMethod : ISendMessageExtensionsTests
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCalledWithMoreThanFiveMessages()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of messages is 5.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[6]);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenArrarHasNullValue()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The message should not be null.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[1] { null });
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMessageTypeIsInvalid()
            {
                ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
                {
                    ISendMessageExtensions.ValidateAndConvert(new ISendMessage[1] { new InvalidMessage() });
                });
            }

            [TestMethod]
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
