// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests.Messages.Template
{
    public partial class ButtonsTemplateTests
    {
        [TestClass]
        public class TheTextProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenSetToNull()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    template.Text = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsEmpty()
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
