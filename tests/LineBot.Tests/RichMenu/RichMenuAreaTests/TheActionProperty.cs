// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuAreaTests
    {
        [TestClass]
        public class TheActionProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var richMenuArea = new RichMenuArea();

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    richMenuArea.Action = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNotNull()
            {
                var action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") };

                var richMenuArea = new RichMenuArea
                {
                    Action = action
                };

                Assert.AreEqual(action, richMenuArea.Action);
            }
        }
    }
}