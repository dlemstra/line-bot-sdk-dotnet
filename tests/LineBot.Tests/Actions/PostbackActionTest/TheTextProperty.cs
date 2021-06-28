// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class PostbackActionTest
    {
        [TestClass]
        public class TheTextProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var action = new PostbackAction
                {
                    Text = null
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan300Chars()
            {
                var action = new PostbackAction();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 300 characters.", () =>
                {
                    action.Text = new string('x', 301);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs300Chars()
            {
                var value = new string('x', 300);

                var action = new PostbackAction()
                {
                    Text = value
                };

                Assert.AreEqual(value, action.Text);
            }
        }
    }
}