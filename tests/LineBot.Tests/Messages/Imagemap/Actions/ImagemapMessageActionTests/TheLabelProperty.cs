// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapMessageActionTests
    {
        [TestClass]
        public class TheLabelProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan50Chars()
            {
                var action = new ImagemapMessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be longer than 50 characters.", () =>
                {
                    action.Label = new string('x', 51);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs50Chars()
            {
                string value = new string('x', 50);

                var action = new ImagemapMessageAction()
                {
                    Label = value
                };

                Assert.AreEqual(value, action.Label);
            }
        }
    }
}
