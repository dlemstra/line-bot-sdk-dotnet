// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapMessageActionTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                var action = new ImagemapMessageAction()
                {
                    Area = new ImagemapArea(1, 2, 3, 4)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenAreaIsNull()
            {
                var action = new ImagemapMessageAction()
                {
                    Text = "test"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    action.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                var action = new ImagemapMessageAction()
                {
                    Text = "test",
                    Area = new ImagemapArea(1, 2, 3, 4)
                };

                action.Validate();
            }
        }
    }
}
