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
    public partial class TemplateMessageTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsTemplateMessage()
            {
                var message = new TemplateMessage()
                {
                    AlternativeText = "Alternative",
                    Template = new ButtonsTemplate()
                    {
                        ThumbnailUrl = new Uri("https://foo.bar"),
                        Title = "ButtonsTitle",
                        Text = "ButtonsText",
                        Actions = new[]
                        {
                            new PostbackAction()
                            {
                                Label = "PostbackLabel",
                                Text = "PostbackText",
                                Data = "PostbackData",
                            }
                        }
                    }
                };

                var templateMessage = TemplateMessage.Convert(message);

                Assert.AreSame(message, templateMessage);

                var buttonsTemplate = templateMessage.Template as IButtonsTemplate;
                Assert.AreSame(message.Template, buttonsTemplate);

                var action = buttonsTemplate.Actions.First();
                Assert.AreSame(action, ((ButtonsTemplate)message.Template).Actions.First());
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAlternativeTextIsNull()
            {
                var message = new TemplateMessage()
                {
                    Template = new ButtonsTemplate()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null.", () =>
                {
                    TemplateMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTemplateIsNull()
            {
                var message = new TemplateMessage()
                {
                    AlternativeText = "AlternativeText"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The template cannot be null.", () =>
                {
                    TemplateMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTemplateIsInvalid()
            {
                var message = new TestTemplateMessage()
                {
                    Template = new InvalidTemplate()
                };

                ExceptionAssert.Throws<NotSupportedException>("Invalid template type.", () =>
                {
                    TemplateMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomITemplateMessageToTemplateMessage()
            {
                var message = new TestTemplateMessage()
                {
                    Template = new TestButtonsTemplate()
                };

                var templateMessage = TemplateMessage.Convert(message);

                Assert.AreNotEqual(message, templateMessage);

                Assert.AreEqual("AlternativeText", templateMessage.AlternativeText);

                var template = templateMessage.Template as ButtonsTemplate;
                Assert.AreEqual(new Uri("https://bar.foo"), template.ThumbnailUrl);
                Assert.AreEqual("ButtonsTitle", template.Title);
                Assert.AreEqual("ButtonsText", template.Text);
            }

            [ExcludeFromCodeCoverage]
            private class InvalidTemplate : IOldTemplate
            {
            }
        }
    }
}
