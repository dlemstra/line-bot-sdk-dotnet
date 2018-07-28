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
    public partial class ButtonsTemplateTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsButtonsTemplate()
            {
                var template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Text = "ButtonsText",
                    Title = "ButtonsTitle",
                    Actions = new ITemplateAction[]
                    {
                        new PostbackAction()
                        {
                            Label = "PostbackLabel",
                            Text = "PostbackText",
                            Data = "PostbackData",
                        }
                    }
                };

                var buttonsTemplate = ButtonsTemplate.Convert(template);

                Assert.AreEqual(template, buttonsTemplate);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                var template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Actions = new ITemplateAction[]
                    {
                        new PostbackAction()
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null.", () =>
                {
                    ButtonsTemplate.Convert(template);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionsIsNull()
            {
                var template = new ButtonsTemplate()
                {
                    ThumbnailUrl = new Uri("https://foo.bar"),
                    Title = "ButtonsTitle",
                    Text = "ButtonsText"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    ButtonsTemplate.Convert(template);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIButtonsTemplateToButtonsTemplate()
            {
                var template = new TestButtonsTemplate();

                var buttonsTemplate = ButtonsTemplate.Convert(template);

                Assert.AreEqual(new Uri("https://bar.foo"), buttonsTemplate.ThumbnailUrl);
                Assert.AreEqual("ButtonsTitle", buttonsTemplate.Title);
                Assert.AreEqual("ButtonsText", buttonsTemplate.Text);

                var actions = buttonsTemplate.Actions.ToArray();

                var action = actions[0] as PostbackAction;
                Assert.AreEqual("PostbackLabel", action.Label);
                Assert.AreEqual("PostbackData", action.Data);
                Assert.AreEqual("PostbackText", action.Text);
            }
        }
    }
}
