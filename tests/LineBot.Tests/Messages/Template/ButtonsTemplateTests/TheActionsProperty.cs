// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class ButtonsTemplateTests
    {
        public class TheActionsProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    template.Actions = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of actions is 1.", () =>
                {
                    template.Actions = new IAction[] { };
                });
            }

            [Fact]
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

            [Fact]
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

            [Fact]
            public void ShouldThrowExceptionWhenTemplateActionTypeIsInvalid()
            {
                var template = new ButtonsTemplate();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    template.Actions = new[] { new TestAction() };
                });
            }

            [Fact]
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
