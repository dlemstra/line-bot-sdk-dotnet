// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

namespace Line.Tests.Messages.Template
{
    public partial class ButtonsTemplateTests
    {
        [TestClass]
        public class TheTitleProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenSetToNull()
            {
                ButtonsTemplate template = new ButtonsTemplate
                {
                    Title = null
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan400Chars()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 400 characters.", () =>
                {
                    template.Title = new string('x', 401);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs400Chars()
            {
                string value = new string('x', 400);

                ButtonsTemplate template = new ButtonsTemplate()
                {
                    Title = value
                };

                Assert.AreEqual(value, template.Title);
            }
        }
    }
}
