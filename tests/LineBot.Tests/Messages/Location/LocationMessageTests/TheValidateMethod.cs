// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LocationMessageTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
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

            [TestMethod]
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

            [TestMethod]
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
