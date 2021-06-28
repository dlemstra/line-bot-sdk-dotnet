// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TemplateMessageTests
    {
        [TestClass]
        public class TheAlternativeTextProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
                {
                    message.AlternativeText = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
                {
                    message.AlternativeText = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan400Chars()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be longer than 400 characters.", () =>
                {
                    message.AlternativeText = new string('x', 401);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs400Chars()
            {
                var value = new string('x', 400);

                var message = new TemplateMessage()
                {
                    AlternativeText = value
                };

                Assert.AreEqual(value, message.AlternativeText);
            }
        }
    }
}