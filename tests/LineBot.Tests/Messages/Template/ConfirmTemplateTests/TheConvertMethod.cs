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
    public partial class ConfirmTemplateTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsCarouselTemplate()
            {
                var template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction()
                    {
                        Label = "PostbackLabel",
                        Text = "PostbackText"
                    },
                    CancelAction = new UriAction()
                    {
                        Label = "PostbackLabel",
                        Url = new Uri("http://foo.bar")
                    }
                };

                var confirmTemplate = ConfirmTemplate.Convert(template);

                Assert.AreEqual(template, confirmTemplate);

                Assert.AreEqual(confirmTemplate.OkAction, template.OkAction);
                Assert.AreEqual(confirmTemplate.CancelAction, template.CancelAction);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                var template = new ConfirmTemplate()
                {
                    OkAction = new MessageAction(),
                    CancelAction = new UriAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    ConfirmTemplate.Convert(template);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenOkActionIsNull()
            {
                var template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    CancelAction = new UriAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The ok action cannot be null.", () =>
                {
                    ConfirmTemplate.Convert(template);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCancelActionIsNull()
            {
                var template = new ConfirmTemplate()
                {
                    Text = "ConfirmText",
                    OkAction = new MessageAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The cancel action cannot be null.", () =>
                {
                    ConfirmTemplate.Convert(template);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIConfirmTemplateToConfirmTemplate()
            {
                var template = new TestConfirmTemplate();

                var confirmTemplate = ConfirmTemplate.Convert(template);
                Assert.AreEqual("ConfirmText", template.Text);

                var okAction = confirmTemplate.OkAction as MessageAction;
                Assert.AreEqual("MessageLabel", okAction.Label);
                Assert.AreEqual("MessageText", okAction.Text);

                var cancelAction = confirmTemplate.CancelAction as UriAction;
                Assert.AreEqual("UriLabel", cancelAction.Label);
                Assert.AreEqual(new Uri("tel://uri"), cancelAction.Url);
            }
        }
    }
}
