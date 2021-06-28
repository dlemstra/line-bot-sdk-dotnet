// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class IActionExtensionsTests
    {
        [TestClass]
        public class TheCheckActionTypeMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsInvalid()
            {
                var action = new TestAction();

                ExceptionAssert.Throws<NotSupportedException>(() => IActionExtensions.CheckActionType(action));
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenActionIsPostbackAction()
            {
                var action = new PostbackAction();

                IActionExtensions.CheckActionType(action);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenActionIsMessageAction()
            {
                var action = new MessageAction();

                IActionExtensions.CheckActionType(action);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenActionIsUriAction()
            {
                var action = new UriAction();

                IActionExtensions.CheckActionType(action);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenActionIsCameraAction()
            {
                var action = new CameraAction();

                IActionExtensions.CheckActionType(action);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenActionIsCameraRollAction()
            {
                var action = new CameraRollAction();

                IActionExtensions.CheckActionType(action);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenActionIsLocationAction()
            {
                var action = new LocationAction();

                IActionExtensions.CheckActionType(action);
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenActionIsDateTimePickerAction()
            {
                var action = new DateTimePickerAction(DateTimePickerMode.Date);

                IActionExtensions.CheckActionType(action);
            }
        }
    }
}
