// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        public class TheTextProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = string.Empty;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan160Chars()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 160 characters.", () =>
                {
                    template.Text = new string('x', 161);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs160Chars()
            {
                var value = new string('x', 160);

                var template = new ButtonsTemplate()
                {
                    Text = value
                };

                Assert.Equal(value, template.Text);
            }

            [Fact]
            public void ShouldThrowExceptionWhenThumbnailUrlSetAndValueIsMoreThan60Chars()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    template.ThumbnailUrl = new Uri("https://foo.bar/");
                    template.Text = new string('x', 61);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenThumbnailUrlSetAndValueIs60Chars()
            {
                var template = new ButtonsTemplate
                {
                    ThumbnailUrl = new Uri("https://foo.bar/"),
                    Text = new string('x', 60)
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenTitleSetAndValueIsMoreThan60Chars()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    template.Title = "Test";
                    template.Text = new string('x', 61);
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenTitleSetAndValueIs60Chars()
            {
                var template = new ButtonsTemplate
                {
                    Title = "Test",
                    Text = new string('x', 60)
                };
            }
        }
    }
}
