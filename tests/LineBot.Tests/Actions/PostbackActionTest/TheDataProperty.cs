// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class PostbackActionTest
    {
        [TestClass]
        public class TheDataProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var action = new PostbackAction();

                ExceptionAssert.Throws<InvalidOperationException>("The data cannot be null or whitespace.", () =>
                {
                    action.Data = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var action = new PostbackAction();

                ExceptionAssert.Throws<InvalidOperationException>("The data cannot be null or whitespace.", () =>
                {
                    action.Data = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan300Chars()
            {
                var action = new PostbackAction();

                ExceptionAssert.Throws<InvalidOperationException>("The data cannot be longer than 300 characters.", () =>
                {
                    action.Data = new string('x', 301);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs300Chars()
            {
                var value = new string('x', 300);

                var action = new PostbackAction()
                {
                    Data = value
                };

                Assert.AreEqual(value, action.Data);
            }
        }
    }
}