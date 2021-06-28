// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImagemapUriActionTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenUrlIsNull()
            {
                var action = new ImagemapUriAction()
                {
                    Area = new ImagemapArea(1, 2, 3, 4)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAreaIsNull()
            {
                var action = new ImagemapUriAction()
                {
                    Url = new Uri("http://foo.bar")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                var action = new ImagemapUriAction()
                {
                    Area = new ImagemapArea(1, 2, 3, 4),
                    Url = new Uri("http://foo.bar")
                };

                action.Validate();
            }
        }
    }
}
