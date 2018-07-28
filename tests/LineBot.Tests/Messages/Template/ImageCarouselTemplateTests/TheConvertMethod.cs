// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageCarouselTemplateTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsCarouselTemplate()
            {
                var template = new ImageCarouselTemplate()
                {
                    Columns = new IImageCarouselColumn[]
                    {
                        new ImageCarouselColumn()
                        {
                            ImageUrl = new Uri("https://foo.bar"),
                            Action = new MessageAction()
                            {
                                Label = "PostbackLabel",
                                Text = "PostbackText"
                            }
                        }
                    }
                };

                var carouselTemplate = ImageCarouselTemplate.Convert(template);

                Assert.AreEqual(template, carouselTemplate);

                var column = carouselTemplate.Columns.First();
                Assert.AreEqual(column, carouselTemplate.Columns.First());
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsIsNull()
            {
                var template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    ImageCarouselTemplate.Convert(template);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomICarouselTemplateToCarouselTemplate()
            {
                var template = new TestImageCarouselTemplate();

                var imageCarouselTemplate = ImageCarouselTemplate.Convert(template);

                Assert.AreNotEqual(template, imageCarouselTemplate);

                var column = imageCarouselTemplate.Columns.First() as ImageCarouselColumn;
                Assert.AreEqual(new Uri("https://carousel.url"), column.ImageUrl);

                var action = column.Action as MessageAction;
                Assert.AreEqual("MessageLabel", action.Label);
                Assert.AreEqual("MessageText", action.Text);
            }
        }
    }
}
