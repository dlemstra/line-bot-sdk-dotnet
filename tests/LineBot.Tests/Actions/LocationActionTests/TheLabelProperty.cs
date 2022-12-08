// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class LocationActionTests
    {
        public class TheLabelProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var action = new LocationAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
                {
                    action.Label = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var action = new LocationAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
                {
                    action.Label = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan20Chars()
            {
                var action = new LocationAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be longer than 20 characters.", () =>
                {
                    action.Label = new string('x', 21);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs20Chars()
            {
                var value = new string('x', 20);

                var action = new LocationAction()
                {
                    Label = value
                };

                Assert.Equal(value, action.Label);
            }
        }
    }
}
