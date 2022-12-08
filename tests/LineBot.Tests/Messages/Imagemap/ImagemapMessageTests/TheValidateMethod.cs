// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ImagemapMessageTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenBaseUrlIsNull()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenBaseSizeIsNull()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The base size cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenBaseSizeIsInvalid()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenAlternativeTextIsNull()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4)
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionsIsNull()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionsAreInvalid()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction()
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction()
                        {
                            Area = new ImagemapArea(1, 2, 3, 4),
                            Text = "Foo"
                        }
                    }
                };

                message.Validate();
            }
        }
    }
}
