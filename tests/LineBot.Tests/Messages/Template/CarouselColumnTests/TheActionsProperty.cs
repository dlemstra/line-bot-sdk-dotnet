// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Xunit;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        public class TheActionsProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    column.Actions = null;
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of actions is 1.", () =>
                {
                    column.Actions = new IAction[] { };
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueContainsMoreThan4Items()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The maximum number of actions is 3.", () =>
                {
                    column.Actions = new[]
                    {
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
                var column = new CarouselColumn()
                {
                    Actions = new IAction[]
                    {
                        new PostbackAction(),
                        new MessageAction(),
                        new UriAction()
                    }
                };
            }

            [Fact]
            public void ShouldThrowExceptionWhenTemplateActionTypeIsInvalid()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    column.Actions = new[] { new TestAction() };
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenCollectionContainsNull()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<NotSupportedException>("The action cannot be null.", () =>
                {
                    column.Actions = new IAction[] { null };
                });
            }
        }
    }
}
