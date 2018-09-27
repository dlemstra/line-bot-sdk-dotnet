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
        public class TheActionsProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    template.Actions = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of actions is 1.", () =>
                {
                    template.Actions = new ITemplateAction[] { };
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueContainsMoreThan4Items()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of actions is 4.", () =>
                {
                    template.Actions = new ITemplateAction[]
                    {
                        new PostbackAction(),
                        new PostbackAction(),
                        new PostbackAction(),
                        new PostbackAction(),
                        new PostbackAction()
                    };
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueContains4Items()
            {
                var template = new ButtonsTemplate()
                {
                    Actions = new ITemplateAction[]
                    {
                        new PostbackAction(),
                        new MessageAction(),
                        new UriAction(),
                        new PostbackAction()
                    }
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTemplateActionTypeIsInvalid()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<NotSupportedException>("The template action type is invalid. Supported types are: PostbackAction, MessageAction and UriAction.", () =>
                {
                    template.Actions = new ITemplateAction[] { new TestTemplateAction() };
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionContainsNull()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<NotSupportedException>("The template action type is invalid. Supported types are: PostbackAction, MessageAction and UriAction.", () =>
                {
                    template.Actions = new ITemplateAction[] { null };
                });
            }
        }
    }
}
