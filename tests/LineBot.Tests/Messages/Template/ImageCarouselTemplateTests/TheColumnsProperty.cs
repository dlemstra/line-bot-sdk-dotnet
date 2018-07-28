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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ImageCarouselTemplateTests
    {
        [TestClass]
        public class TheColumnsProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenSetToNull()
            {
                ImageCarouselTemplate template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The columns cannot be null.", () =>
                {
                    template.Columns = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                ImageCarouselTemplate template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of columns is 1.", () =>
                {
                    template.Columns = new IImageCarouselColumn[] { };
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueContainsMoreThan10Items()
            {
                ImageCarouselTemplate template = new ImageCarouselTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of columns is 10.", () =>
                {
                    template.Columns = new IImageCarouselColumn[]
                    {
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn()
                    };
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueContains10Items()
            {
                ImageCarouselTemplate template = new ImageCarouselTemplate()
                {
                    Columns = new IImageCarouselColumn[]
                    {
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn(),
                        new ImageCarouselColumn()
                    }
                };
            }
        }
    }
}
