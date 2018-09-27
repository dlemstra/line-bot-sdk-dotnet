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
    public partial class RichMenuAreaTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsNull()
            {
                var richMenuArea = new RichMenuArea();

                ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenActionIsInvalid()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction(),
                    Bounds = new RichMenuBounds()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenBoundsIsNull()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The bounds cannot be null.", () =>
                {
                    richMenuArea.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenBoundsIsInvalid()
            {
                var richMenuArea = new RichMenuArea()
                {
                    Action = new MessageAction() { Label = "Foo", Text = "Bar" },
                    Bounds = new RichMenuBounds()
                };

                ExceptionAssert.Throws<InvalidOperationException>("The width is not set.", () =>
                {
                    richMenuArea.Validate();
                });
            }
        }
    }
}
