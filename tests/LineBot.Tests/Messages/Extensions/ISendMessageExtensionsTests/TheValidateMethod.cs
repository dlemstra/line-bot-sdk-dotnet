// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ISendMessageExtensionsTests
    {
        public class TheValidateMethod : ISendMessageExtensionsTests
        {
            [Fact]
            public void ShouldThrowExceptionWhenCalledWithMoreThanFiveMessages()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of messages is 5.", () =>
                {
                    ISendMessageExtensions.Validate(new ISendMessage[6]);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrarHasNullValue()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The message should not be null.", () =>
                {
                    ISendMessageExtensions.Validate(new ISendMessage[1] { null });
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenMessageTypeIsInvalid()
            {
                ExceptionAssert.Throws<NotSupportedException>("Invalid message type.", () =>
                {
                    ISendMessageExtensions.Validate(new ISendMessage[1] { new InvalidMessage() });
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenMessageIsInvalid()
            {
                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    ISendMessageExtensions.Validate(new ISendMessage[1] { new TextMessage() });
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMessageIsTextMessage()
            {
                var message = new TextMessage() { Text = "Foo" };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMessageIsImageMessage()
            {
                var message = new ImageMessage()
                {
                    Url = new Uri("https://foo.bar"),
                    PreviewUrl = new Uri("https://bar.foo")
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMessageIsVideoMessage()
            {
                var message = new VideoMessage()
                {
                    Url = new Uri("https://foo.bar"),
                    PreviewUrl = new Uri("https://bar.foo")
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMessageIsAudioMessage()
            {
                var message = new AudioMessage()
                {
                    Url = new Uri("https://foo.bar"),
                    Duration = 1
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMessageIsLocationMessage()
            {
                var message = new LocationMessage()
                {
                    Title = "Foo",
                    Address = "Bar"
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMessageIsStickerMessage()
            {
                var message = new StickerMessage()
                {
                    PackageId = "1",
                    StickerId = "2"
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMessageIsImagemapMessage()
            {
                var message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    AlternativeText = "Foo",
                    BaseSize = new ImagemapSize(10, 10),
                    Actions = new[] { new ImagemapUriAction("https://foo.bar", new ImagemapArea(1, 2, 3, 4)) }
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenMessageIsTemplateMessage()
            {
                var message = new TemplateMessage()
                {
                    AlternativeText = "Foo",
                    Template = new ButtonsTemplate()
                    {
                        Text = "Foo",
                        Actions = new[] { new MessageAction() { Label = "Foo", Text = "Bar" } }
                    }
                };

                ISendMessageExtensions.Validate(new ISendMessage[1] { message });
            }
        }
    }
}
