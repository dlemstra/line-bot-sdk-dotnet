// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class UriActionTests
    {
        public class TheValidateMethod
        {
            [Fact]
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

            [Fact]
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

            [Fact]
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
