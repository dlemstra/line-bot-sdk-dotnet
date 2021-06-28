// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        [TestClass]
        public class TheDefaultActionProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var column = new CarouselColumn
                {
                    DefaultAction = null
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsInvalid()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    column.DefaultAction = new TestAction();
                });
            }
        }
    }
}
