// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class MessageActionTests
    {
        [TestClass]
        public class TheLabelProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var action = new MessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
                {
                    action.Label = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var action = new MessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
                {
                    action.Label = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan20Chars()
            {
                var action = new MessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be longer than 20 characters.", () =>
                {
                    action.Label = new string('x', 21);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs20Chars()
            {
                var value = new string('x', 20);

                var action = new MessageAction()
                {
                    Label = value
                };

                Assert.AreEqual(value, action.Label);
            }
        }
    }
}