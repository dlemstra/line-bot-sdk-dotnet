// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class VideoMessageTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenUrlIsNull()
            {
                ISendMessage message = new VideoMessage()
                {
                    PreviewUrl = new Uri("https://foo.previewUrl")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenPreviewUrlIsNull()
            {
                ISendMessage message = new VideoMessage()
                {
                    Url = new Uri("https://foo.url")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The preview url cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ISendMessage message = new VideoMessage()
                {
                    PreviewUrl = new Uri("https://foo.previewUrl"),
                    Url = new Uri("https://foo.url")
                };

                message.Validate();
            }
        }
    }
}
