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
    public partial class ImageCarouselColumnTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsImageCarouselColumn()
            {
                var column = new ImageCarouselColumn()
                {
                    ImageUrl = new Uri("https://foo.bar"),
                    Action = new PostbackAction()
                    {
                        Label = "PostbackLabel",
                        Text = "PostbackText",
                        Data = "PostbackData",
                    }
                };

                var columns = ImageCarouselColumn.Convert(new[] { column }).ToArray();

                Assert.AreEqual(1, columns.Length);
                Assert.AreSame(column, columns[0]);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImageUrlIsNull()
            {
                var column = new ImageCarouselColumn()
                {
                    Action = new PostbackAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The image url cannot be null.", () =>
                {
                    ImageCarouselColumn.Convert(new[] { column }).ToArray();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsNull()
            {
                var column = new ImageCarouselColumn()
                {
                    ImageUrl = new Uri("https://foo.bar"),
                };

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    ImageCarouselColumn.Convert(new[] { column }).ToArray();
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIImageCarouselColumnToImageCarouselColumn()
            {
                var column = new TestImageCarouselColumn();

                var columns = ImageCarouselColumn.Convert(new[] { column }).ToArray();

                Assert.AreEqual(1, columns.Length);
                Assert.AreNotEqual(column, columns[0]);

                var carouselColumn = columns[0] as ImageCarouselColumn;

                Assert.AreEqual(new Uri("https://carousel.url"), carouselColumn.ImageUrl);
            }
        }
    }
}
