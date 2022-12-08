// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImageMessageTests
    {
        public class ThePreviewUrlProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new ImageMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    message.PreviewUrl = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                var message = new ImageMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The url should use the https scheme.", () =>
                {
                    message.PreviewUrl = new Uri("http://foo.bar");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var message = new ImageMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 1000 characters.", () =>
                {
                    message.PreviewUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenUrlIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var message = new ImageMessage()
                {
                    PreviewUrl = value
                };

                Assert.Equal(value, message.PreviewUrl);
            }
        }
    }
}
