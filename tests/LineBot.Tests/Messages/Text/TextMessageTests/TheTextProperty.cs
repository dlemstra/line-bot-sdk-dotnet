// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TextMessageTests
    {
        [TestClass]
        public class TheTextProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new TextMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    message.Text = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new TextMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    message.Text = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan2000Chars()
            {
                var message = new TextMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 2000 characters.", () =>
                {
                    message.Text = new string('x', 2001);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs2000Chars()
            {
                var value = new string('x', 2000);

                var message = new TextMessage()
                {
                    Text = value
                };

                Assert.AreEqual(value, message.Text);
            }
        }
    }
}
