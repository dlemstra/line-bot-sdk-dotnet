// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ConfirmTemplateTests
    {
        public class TheCancelActionProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The cancel action cannot be null.", () =>
                {
                    template.CancelAction = null;
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNotNull()
            {
                var action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") };

                var confirmTemplate = new ConfirmTemplate
                {
                    CancelAction = action
                };

                Assert.Equal(action, confirmTemplate.CancelAction);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsInvalidType()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    template.CancelAction = new TestAction();
                });
            }
        }
    }
}
