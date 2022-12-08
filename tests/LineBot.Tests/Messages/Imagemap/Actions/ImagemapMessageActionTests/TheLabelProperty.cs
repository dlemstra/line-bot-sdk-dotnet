// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapMessageActionTests
    {
        public class TheLabelProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan50Chars()
            {
                var action = new ImagemapMessageAction();

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be longer than 50 characters.", () =>
                {
                    action.Label = new string('x', 51);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs50Chars()
            {
                var value = new string('x', 50);

                var action = new ImagemapMessageAction()
                {
                    Label = value
                };

                Assert.Equal(value, action.Label);
            }
        }
    }
}
