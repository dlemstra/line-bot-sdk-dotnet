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