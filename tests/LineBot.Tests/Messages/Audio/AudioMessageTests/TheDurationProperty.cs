// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class AudioMessageTests
    {
        [TestClass]
        public class TheDurationProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsZero()
            {
                AudioMessage message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
                {
                    message.Duration = 0;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMinusOne()
            {
                AudioMessage message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
                {
                    message.Duration = -1;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan59999()
            {
                AudioMessage message = new AudioMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The duration cannot be longer than 1 minute.", () =>
                {
                    message.Duration = 60000;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenUrlIs59999Milliseconds()
            {
                int value = 59999;

                AudioMessage message = new AudioMessage()
                {
                    Duration = value
                };

                Assert.AreEqual(value, message.Duration);
            }
        }
    }
}
