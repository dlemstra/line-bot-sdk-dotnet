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
    public partial class ButtonsTemplateTests
    {
        [TestClass]
        public class TheImageBackgroundColorProperty
        {
            [TestMethod]
            public void ShouldThowExceptionWhenValueIsNull()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color should be 7 characters long.", () =>
                {
                    template.ImageBackgroundColor = null;
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsToLong()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color should be 7 characters long.", () =>
                {
                    template.ImageBackgroundColor = "#FFFFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueNotStartsWithNumberSign()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color should start with #.", () =>
                {
                    template.ImageBackgroundColor = "FFFFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#@FFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid2()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#GFFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid3()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#`FFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid4()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#gFFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid5()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#/FFFFF";
                });
            }

            [TestMethod]
            public void ShouldThowExceptionWhenValueIsInvalid6()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The color contains invalid characters.", () =>
                {
                    template.ImageBackgroundColor = "#:FFFFF";
                });
            }

            [TestMethod]
            public void ShouldUppercaseTheValue()
            {
                ButtonsTemplate template = new ButtonsTemplate
                {
                    ImageBackgroundColor = "#ff00FF"
                };

                Assert.AreEqual("#FF00FF", template.ImageBackgroundColor);
            }
        }
    }
}
