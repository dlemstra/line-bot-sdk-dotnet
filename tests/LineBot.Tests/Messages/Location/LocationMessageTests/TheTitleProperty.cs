// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LocationMessageTests
    {
        [TestClass]
        public class TheTitleProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new LocationMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be null or whitespace.", () =>
                {
                    message.Title = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new LocationMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be null or whitespace.", () =>
                {
                    message.Title = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan100Chars()
            {
                var message = new LocationMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 100 characters.", () =>
                {
                    message.Title = new string('x', 101);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs100Chars()
            {
                var value = new string('x', 100);

                var message = new LocationMessage()
                {
                    Title = value
                };

                Assert.AreEqual(value, message.Title);
            }
        }
    }
}
