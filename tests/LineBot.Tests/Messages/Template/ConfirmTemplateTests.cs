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
using Newtonsoft.Json;

namespace Line.Tests
{
    [TestClass]
    public class ConfirmTemplateTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            ConfirmTemplate template = new ConfirmTemplate
            {
                Text = "Test",
                OkAction = new MessageAction()
                {
                    Label = "OkLabel",
                    Text = "OkText"
                },
                CancelAction = new MessageAction()
                {
                    Label = "CancelLabel",
                    Text = "CancelText"
                }
            };

            string serialized = JsonConvert.SerializeObject(template);
            Assert.AreEqual(@"{""type"":""confirm"",""text"":""Test"",""actions"":[{""type"":""message"",""label"":""OkLabel"",""text"":""OkText""},{""type"":""message"",""label"":""CancelLabel"",""text"":""CancelText""}]}", serialized);
        }

        [TestMethod]
        public void Text_Null_ThrowsException()
        {
            ConfirmTemplate template = new ConfirmTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                template.Text = null;
            });
        }

        [TestMethod]
        public void Text_Empty_ThrowsException()
        {
            ConfirmTemplate template = new ConfirmTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                template.Text = string.Empty;
            });
        }

        [TestMethod]
        public void Text_MoreThan240Chars_ThrowsException()
        {
            ConfirmTemplate template = new ConfirmTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 240 characters.", () =>
            {
                template.Text = new string('x', 241);
            });
        }

        [TestMethod]
        public void Text_240Chars_ThrowsNoException()
        {
            string value = new string('x', 240);

            ConfirmTemplate template = new ConfirmTemplate()
            {
                Text = value
            };

            Assert.AreEqual(value, template.Text);
        }

        [TestMethod]
        public void OkAction_Null_ThrowsException()
        {
            ConfirmTemplate template = new ConfirmTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The ok action cannot be null.", () =>
            {
                template.OkAction = null;
            });
        }

        [TestMethod]
        public void OkAction_InvalidTemplateActionType_ThrowsException()
        {
            ConfirmTemplate template = new ConfirmTemplate();

            ExceptionAssert.Throws<NotSupportedException>("The template action type is invalid. Supported types are: IPostbackAction, IMessageAction and IUriAction.", () =>
            {
                template.OkAction = new TestTemplateAction();
            });
        }

        [TestMethod]
        public void CancelAction_Null_ThrowsException()
        {
            ConfirmTemplate template = new ConfirmTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The cancel action cannot be null.", () =>
            {
                template.CancelAction = null;
            });
        }

        [TestMethod]
        public void CancelAction_InvalidTemplateActionType_ThrowsException()
        {
            ConfirmTemplate template = new ConfirmTemplate();

            ExceptionAssert.Throws<NotSupportedException>("The template action type is invalid. Supported types are: IPostbackAction, IMessageAction and IUriAction.", () =>
            {
                template.CancelAction = new TestTemplateAction();
            });
        }
    }
}
