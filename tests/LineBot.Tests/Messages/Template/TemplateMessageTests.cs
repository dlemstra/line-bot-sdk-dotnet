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
using Newtonsoft.Json;

namespace Line.Tests
{
    [TestClass]
    public class TemplateMessageTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = "Alternative"
            };

            string serialized = JsonConvert.SerializeObject(message);
            Assert.AreEqual(@"{""type"":""template"",""altText"":""Alternative"",""template"":null}", serialized);
        }

        [TestMethod]
        public void AlternativeText_Null_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
            {
                message.AlternativeText = null;
            });
        }

        [TestMethod]
        public void AlternativeText_Empty_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be null or whitespace.", () =>
            {
                message.AlternativeText = string.Empty;
            });
        }

        [TestMethod]
        public void AlternativeText_MoreThan400Chars_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The alternative text cannot be longer than 400 characters.", () =>
            {
                message.AlternativeText = new string('x', 401);
            });
        }

        [TestMethod]
        public void AlternativeText_400Chars_ThrowsNoException()
        {
            string value = new string('x', 400);

            TemplateMessage message = new TemplateMessage()
            {
                AlternativeText = value
            };

            Assert.AreEqual(value, message.AlternativeText);
        }

        [TestMethod]
        public void Template_Null_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The template cannot be null.", () =>
            {
                message.Template = null;
            });
        }

        [TestMethod]
        public void Template_InvalidTemplateType_ThrowsException()
        {
            TemplateMessage message = new TemplateMessage();

            ExceptionAssert.Throws<InvalidOperationException>("The template type is invalid.", () =>
            {
                message.Template = new TestTemplate();
            });
        }

        [TestMethod]
        public void Template_SetToButtonsTemplate_ThrowsNoException()
        {
            TemplateMessage message = new TemplateMessage()
            {
                Template = new ButtonsTemplate()
            };
        }

        [ExcludeFromCodeCoverage]
        private class TestTemplate : ITemplate
        {
        }
    }
}
