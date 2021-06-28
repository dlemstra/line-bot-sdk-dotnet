// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class CarouselColumnTests
    {
        [TestClass]
        public class TheActionsProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    column.Actions = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The minimum number of actions is 1.", () =>
                {
                    column.Actions = new IAction[] { };
                });
            }

            [TestMethod]
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

            [TestMethod]
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

            [TestMethod]
            public void ShouldThrowExceptionWhenTemplateActionTypeIsInvalid()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<NotSupportedException>("The action type is invalid.", () =>
                {
                    column.Actions = new[] { new TestAction() };
                });
            }

            [TestMethod]
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
