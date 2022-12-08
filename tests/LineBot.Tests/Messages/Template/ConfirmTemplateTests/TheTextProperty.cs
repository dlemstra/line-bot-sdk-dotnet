// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ConfirmTemplateTests
    {
        public class TheTextProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan240Chars()
            {
                var template = new ConfirmTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 240 characters.", () =>
                {
                    template.Text = new string('x', 241);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs240Chars()
            {
                var value = new string('x', 240);

                var template = new ConfirmTemplate()
                {
                    Text = value
                };

                Assert.Equal(value, template.Text);
            }
        }
    }
}
