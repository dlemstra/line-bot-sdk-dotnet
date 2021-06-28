// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class UriActionTests
    {
        [TestClass]
        public class TheUrlProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var action = new UriAction();

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    action.Url = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNotHttpOrHttpsOrLineOrTel()
            {
                var action = new UriAction();

                ExceptionAssert.Throws<InvalidOperationException>("The url should use the http, https, line or tel scheme.", () =>
                {
                    action.Url = new Uri("ftp://foo.bar");
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsHttp()
            {
                var action = new UriAction
                {
                    Url = new Uri("http://foo.bar")
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsHttps()
            {
                var action = new UriAction
                {
                    Url = new Uri("https://foo.bar")
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsLine()
            {
                var action = new UriAction
                {
                    Url = new Uri("line://nv/camera/")
                };
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsTel()
            {
                var action = new UriAction
                {
                    Url = new Uri("tel://1234")
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                var action = new UriAction();

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 1000 characters.", () =>
                {
                    action.Url = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs1000Chars()
            {
                var value = new Uri("http://foo.bar/" + new string('x', 984));

                var action = new UriAction()
                {
                    Url = value
                };

                Assert.AreEqual(value, action.Url);
            }
        }
    }
}