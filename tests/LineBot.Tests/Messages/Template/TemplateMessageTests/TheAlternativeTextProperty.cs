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
    public partial class TemplateMessageTests
    {
        [TestClass]
        public class TheAlternativeTextProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
                {
                    message.AlternativeText = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
                {
                    message.AlternativeText = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan400Chars()
            {
                var message = new TemplateMessage();

                ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be longer than 400 characters.", () =>
                {
                    message.AlternativeText = new string('x', 401);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs400Chars()
            {
                var value = new string('x', 400);

                var message = new TemplateMessage()
                {
                    AlternativeText = value
                };

                Assert.AreEqual(value, message.AlternativeText);
            }
        }
    }
}