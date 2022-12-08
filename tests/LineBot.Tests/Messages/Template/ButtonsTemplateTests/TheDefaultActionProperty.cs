// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        public class TheDefaultActionProperty
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var template = new ButtonsTemplate
                {
                    DefaultAction = null
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsInvalid()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    template.DefaultAction = new TestAction();
                });
            }
        }
    }
}
