// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapMessageTests
    {
        public class TheBaseUrlProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be null.", () =>
                {
                    message.BaseUrl = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The base url should use the https scheme.", () =>
                {
                    message.BaseUrl = new Uri("http://foo.bar");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var message = new ImagemapMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be longer than 1000 characters.", () =>
                {
                    message.BaseUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenUrlIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var message = new ImagemapMessage()
                {
                    BaseUrl = value
                };

                Assert.Equal(value, message.BaseUrl);
            }
        }
    }
}
