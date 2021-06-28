// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class UriActionTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenLabelIsNull()
            {
                IAction action = new UriAction()
                {
                    Url = new Uri("https://foo.bar")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenUriIsNull()
            {
                IAction action = new UriAction()
                {
                    Label = "UriLabel"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                IAction action = new UriAction()
                {
                    Url = new Uri("https://foo.bar"),
                    Label = "UriLabel"
                };

                action.Validate();
            }
        }
    }
}
