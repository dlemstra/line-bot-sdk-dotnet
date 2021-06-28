// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        [TestClass]
        public class TheTextProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan160Chars()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 160 characters.", () =>
                {
                    template.Text = new string('x', 161);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs160Chars()
            {
                string value = new string('x', 160);

                ButtonsTemplate template = new ButtonsTemplate()
                {
                    Text = value
                };

                Assert.AreEqual(value, template.Text);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenThumbnailUrlSetAndValueIsMoreThan60Chars()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    template.ThumbnailUrl = new Uri("https://foo.bar/");
                    template.Text = new string('x', 61);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenThumbnailUrlSetAndValueIs60Chars()
            {
                ButtonsTemplate template = new ButtonsTemplate
                {
                    ThumbnailUrl = new Uri("https://foo.bar/"),
                    Text = new string('x', 60)
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTitleSetAndValueIsMoreThan60Chars()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    template.Title = "Test";
                    template.Text = new string('x', 61);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenTitleSetAndValueIs60Chars()
            {
                ButtonsTemplate template = new ButtonsTemplate
                {
                    Title = "Test",
                    Text = new string('x', 60)
                };
            }
        }
    }
}
