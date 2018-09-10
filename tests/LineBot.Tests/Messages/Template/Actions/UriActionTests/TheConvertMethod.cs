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
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsUriAction()
            {
                var action = new PostbackAction()
                {
                    Label = "PostbackLabel",
                    Data = "PostbackData",
                    Text = "PostbackText",
                };

                var messageAction = PostbackAction.Convert(action);

                Assert.AreSame(action, messageAction);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenLabelIsNull()
            {
                var action = new UriAction()
                {
                    Url = new Uri("https://foo.bar")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    UriAction.Convert(action);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenUriIsNull()
            {
                var action = new UriAction()
                {
                    Label = "UriLabel"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    UriAction.Convert(action);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIUriActioneToUriAction()
            {
                var action = new TestUriAction();

                var uriAction = UriAction.Convert(action);

                Assert.AreNotEqual(action, uriAction);

                Assert.AreEqual("UriLabel", uriAction.Label);
                Assert.AreEqual("tel://uri/", uriAction.Url.ToString());
            }
        }
    }
}
