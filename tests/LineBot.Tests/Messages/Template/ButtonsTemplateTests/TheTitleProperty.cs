// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        public class TheTitleProperty
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var template = new ButtonsTemplate
                {
                    Title = null
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan400Chars()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 400 characters.", () =>
                {
                    template.Title = new string('x', 401);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs400Chars()
            {
                var value = new string('x', 400);

                var template = new ButtonsTemplate()
                {
                    Title = value
                };

                Assert.Equal(value, template.Title);
            }
        }
    }
}
