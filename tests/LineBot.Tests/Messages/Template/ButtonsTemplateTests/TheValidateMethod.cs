// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        public class TheValidateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                ITemplate template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Actions = new[]
                    {
                        new PostbackAction()
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionsIsNull()
            {
                ITemplate template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Text = "ButtonsText"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenActionsIsInvalid()
            {
                ITemplate template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Text = "ButtonsText",
                    Actions = new[]
                    {
                        new PostbackAction()
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    template.Validate();
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ITemplate template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Text = "ButtonsText",
                    Actions = new[]
                    {
                        new PostbackAction() { Data = "Foo", Label = "Bar" }
                    }
                };

                template.Validate();
            }
        }
    }
}
