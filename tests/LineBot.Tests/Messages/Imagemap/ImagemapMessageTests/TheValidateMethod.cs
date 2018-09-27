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
    public partial class ImagemapMessageTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenBaseUrlIsNull()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The base url cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenBaseSizeIsNull()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The base size cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenBaseSizeIsInvalid()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4),
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAlternativeTextIsNull()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    Actions = new[]
                    {
                        new ImagemapMessageAction("Text", 1, 2, 3, 4)
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionsIsNull()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionsAreInvalid()
            {
                ISendMessage message = new ImagemapMessage()
                {
                    BaseUrl = new Uri("https://foo.bar"),
                    BaseSize = new ImagemapSize(1040, 1040),
                    AlternativeText = "Alternative",
                    Actions = new[]
                    {
                        new ImagemapMessageAction()
                    }
                };

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    message.Validate();
                });
            }
        }
    }
}
