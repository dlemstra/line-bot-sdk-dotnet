// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
                    template.Actions = new IAction[] { };
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueContainsMoreThan4Items()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of actions is 4.", () =>
                {
                    template.Actions = new[]
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
                    Actions = new IAction[]
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

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    template.Actions = new[] { new TestAction() };
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionContainsNull()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<NotSupportedException>("The action cannot be null.", () =>
                {
                    template.Actions = new IAction[] { null };
                });
            }
        }
    }
}
