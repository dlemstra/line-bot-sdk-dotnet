// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class LocationMessageTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenTitleIsNull()
            {
                ISendMessage message = new LocationMessage()
                {
                    Address = "Address"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenAddressIsNull()
            {
                ISendMessage message = new LocationMessage()
                {
                    Title = "Title"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The address cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ISendMessage message = new LocationMessage()
                {
                    Address = "Address",
                    Title = "Title"
                };

                message.Validate();
            }
        }
    }
}
