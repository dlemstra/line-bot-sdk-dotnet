// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        [TestClass]
        public class TheTitleProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                ButtonsTemplate template = new ButtonsTemplate
                {
                    Title = null
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan400Chars()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 400 characters.", () =>
                {
                    template.Title = new string('x', 401);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs400Chars()
            {
                string value = new string('x', 400);

                ButtonsTemplate template = new ButtonsTemplate()
                {
                    Title = value
                };

                Assert.AreEqual(value, template.Title);
            }
        }
    }
}
