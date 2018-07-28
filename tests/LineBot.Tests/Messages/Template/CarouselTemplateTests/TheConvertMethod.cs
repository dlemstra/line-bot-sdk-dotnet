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

namespace Line.Tests.Messages.Audio
{
    public partial class CarouselTemplateTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsCarouselTemplate()
            {
                var template = new CarouselTemplate()
                {
                    Columns = new ICarouselColumn[]
                    {
                        new CarouselColumn()
                        {
                            ThumbnailUrl = new Uri("https://foo.bar"),
                            Title = "CarouselTitle",
                            Text = "CarouselText",
                            Actions = new ITemplateAction[]
                            {
                                new MessageAction()
                                {
                                    Label = "PostbackLabel",
                                    Text = "PostbackText"
                                }
                            }
                        }
                    }
                };

                var carouselTemplate = CarouselTemplate.Convert(template);

                Assert.AreEqual(template, carouselTemplate);

                var column = carouselTemplate.Columns.First();
                Assert.AreEqual(column, carouselTemplate.Columns.First());
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenColumnsIsNull()
            {
                var template = new CarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    CarouselTemplate.Convert(template);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomICarouselTemplateToCarouselTemplate()
            {
                var template = new TestCarouselTemplate();

                var carouselTemplate = CarouselTemplate.Convert(template);

                Assert.AreNotEqual(template, carouselTemplate);

                var column = carouselTemplate.Columns.First() as CarouselColumn;
                Assert.AreEqual(new Uri("https://carousel.url"), column.ThumbnailUrl);
                Assert.AreEqual("CarouselTitle", column.Title);
                Assert.AreEqual("CarouselText", column.Text);

                var actions = column.Actions.ToArray();

                var action = actions[0] as MessageAction;
                Assert.AreEqual("MessageLabel", action.Label);
                Assert.AreEqual("MessageText", action.Text);
            }
        }
    }
}
