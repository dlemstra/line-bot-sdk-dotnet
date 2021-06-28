// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageCarouselColumnTests
    {
        [TestClass]
        public class TheActionProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var column = new ImageCarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    column.Action = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTemplateActionTypeIsInvalid()
            {
                var column = new ImageCarouselColumn();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    column.Action = new TestAction();
                });
            }
        }
    }
}
