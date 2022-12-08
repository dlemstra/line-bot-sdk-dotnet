// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        public class TheThumbnailUrlProperty
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                var template = new ButtonsTemplate
                {
                    ThumbnailUrl = null
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url should use the https scheme.", () =>
                {
                    template.ThumbnailUrl = new Uri("http://foo.bar");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url cannot be longer than 1000 characters.", () =>
                {
                    template.ThumbnailUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIs1000Chars()
            {
                var value = new Uri("https://foo.bar/" + new string('x', 984));

                var template = new ButtonsTemplate()
                {
                    ThumbnailUrl = value
                };

                Assert.Equal(value, template.ThumbnailUrl);
            }
        }
    }
}
