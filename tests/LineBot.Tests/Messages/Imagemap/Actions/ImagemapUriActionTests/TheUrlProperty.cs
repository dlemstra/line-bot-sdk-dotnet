// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapUriActionTests
    {
        public class TheUrlProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var action = new ImagemapUriAction();

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    action.Url = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var action = new ImagemapUriAction();

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 1000 characters.", () =>
                {
                    action.Url = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenUrlIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var action = new ImagemapUriAction()
                {
                    Url = value
                };

                Assert.Equal(value, action.Url);
            }
        }
    }
}
