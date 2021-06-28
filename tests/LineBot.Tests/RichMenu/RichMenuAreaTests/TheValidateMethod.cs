// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class RichMenuAreaTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsNull()
            {
                var richMenuArea = new RichMenuArea();

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsInvalid()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction(),
                    Bounds = new RichMenuBounds()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenBoundsIsNull()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The bounds cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenBoundsIsInvalid()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction() { Label = "Foo", Text = "Bar" },
                    Bounds = new RichMenuBounds()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The width is not set.", () =>
                {
                    richMenuArea.Validate();
                });
            }
        }
    }
}
