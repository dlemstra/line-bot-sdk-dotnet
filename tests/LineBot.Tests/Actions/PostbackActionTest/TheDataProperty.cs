// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class PostbackActionTest
    {
        public class TheDataProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var action = new PostbackAction();

                ExceptionAssert.Throws<InvalidOperationException>("The data cannot be null or whitespace.", () =>
                {
                    action.Data = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var action = new PostbackAction();

                ExceptionAssert.Throws<InvalidOperationException>("The data cannot be null or whitespace.", () =>
                {
                    action.Data = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan300Chars()
            {
                var action = new PostbackAction();

                ExceptionAssert.Throws<InvalidOperationException>("The data cannot be longer than 300 characters.", () =>
                {
                    action.Data = new string('x', 301);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs300Chars()
            {
                var value = new string('x', 300);

                var action = new PostbackAction()
                {
                    Data = value
                };

                Assert.Equal(value, action.Data);
            }
        }
    }
}
