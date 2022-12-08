﻿// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class LocationMessageTests
    {
        public class TheAddressProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new LocationMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The address cannot be null or whitespace.", () =>
                {
                    message.Address = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new LocationMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The address cannot be null or whitespace.", () =>
                {
                    message.Address = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan100Chars()
            {
                var message = new LocationMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The address cannot be longer than 100 characters.", () =>
                {
                    message.Address = new string('x', 101);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs100Chars()
            {
                var value = new string('x', 100);

                var message = new LocationMessage()
                {
                    Address = value
                };

                Assert.Equal(value, message.Address);
            }
        }
    }
}
