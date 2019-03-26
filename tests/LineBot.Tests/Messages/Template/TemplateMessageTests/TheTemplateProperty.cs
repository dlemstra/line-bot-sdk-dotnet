// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
    public partial class TemplateMessageTests
    {
        [TestClass]
        public class TheTemplateProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The template cannot be null.", () =>
                {
                    message.Template = null;
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsButtonsTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ButtonsTemplate()
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsConfirmTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ConfirmTemplate()
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsCarouselTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new CarouselTemplate()
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsImageCarouselTemplate()
            {
                var message = new TemplateMessage()
                {
                    Template = new ImageCarouselTemplate()
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsInvalid()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The template type is invalid.", () =>
                {
                    message.Template = new TestTemplate();
                });
            }

            [ExcludeFromCodeCoverage]
            private class TestTemplate : ITemplate
            {
                public void Validate()
                {
                }
            }
        }
    }
}
