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
    public class ButtonsTemplateTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            ButtonsTemplate template = new ButtonsTemplate
            {
                ThumbnailUrl = new Uri("https://foo.bar"),
                Title = "Foo",
                Text = "Test"
            };

            string serialized = JsonConvert.SerializeObject(template);
            Assert.AreEqual(@"{""type"":""buttons"",""thumbnailImageUrl"":""https://foo.bar"",""title"":""Foo"",""text"":""Test"",""actions"":null}", serialized);
        }

        [TestMethod]
        public void ThumbnailUrl_SetToNull_ThrowsNoException()
        {
            ButtonsTemplate template = new ButtonsTemplate
            {
                ThumbnailUrl = null
            };
        }

        [TestMethod]
        public void ThumbnailUrl_NotHttps_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url should use the https scheme.", () =>
            {
                template.ThumbnailUrl = new Uri("http://foo.bar");
            });
        }

        [TestMethod]
        public void ThumbnailUrl_MoreThan1000Chars_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url cannot be longer than 1000 characters.", () =>
            {
                template.ThumbnailUrl = new Uri("https://foo.bar/" + new string('x', 985));
            });
        }

        [TestMethod]
        public void ThumbnailUrl_1000Chars_ThrowsNoException()
        {
            Uri value = new Uri("https://foo.bar/" + new string('x', 984));

            ButtonsTemplate template = new ButtonsTemplate()
            {
                ThumbnailUrl = value
            };

            Assert.AreEqual(value, template.ThumbnailUrl);
        }

        [TestMethod]
        public void Title_SetToNull_ThrowsNoException()
        {
            ButtonsTemplate template = new ButtonsTemplate
            {
                Title = null
            };
        }

        [TestMethod]
        public void Title_MoreThan400Chars_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 400 characters.", () =>
            {
                template.Title = new string('x', 401);
            });
        }

        [TestMethod]
        public void Title_400Chars_ThrowsNoException()
        {
            string value = new string('x', 400);

            ButtonsTemplate template = new ButtonsTemplate()
            {
                Title = value
            };

            Assert.AreEqual(value, template.Title);
        }

        [TestMethod]
        public void Text_Null_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                template.Text = null;
            });
        }

        [TestMethod]
        public void Text_Empty_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                template.Text = string.Empty;
            });
        }

        [TestMethod]
        public void Text_MoreThan160Chars_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 160 characters.", () =>
            {
                template.Text = new string('x', 161);
            });
        }

        [TestMethod]
        public void Text_160Chars_ThrowsNoException()
        {
            string value = new string('x', 160);

            ButtonsTemplate template = new ButtonsTemplate()
            {
                Text = value
            };

            Assert.AreEqual(value, template.Text);
        }

        [TestMethod]
        public void TextWithThumbnailUrlSet_MoreThan60Chars_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumnail url or title are set.", () =>
            {
                template.ThumbnailUrl = new Uri("https://foo.bar/");
                template.Text = new string('x', 61);
            });
        }

        [TestMethod]
        public void TextWithThumbnailUrlSet_60Chars_ThrowsNoException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            template.ThumbnailUrl = new Uri("https://foo.bar/");
            template.Text = new string('x', 60);
        }

        [TestMethod]
        public void TextWithTitleSet_MoreThan60Chars_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumnail url or title are set.", () =>
            {
                template.Title = "Test";
                template.Text = new string('x', 61);
            });
        }

        [TestMethod]
        public void TextWithTitleSet_60Chars_ThrowsNoException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            template.Title = "Test";
            template.Text = new string('x', 60);
        }

        [TestMethod]
        public void Actions_Null_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
            {
                template.Actions = null;
            });
        }

        [TestMethod]
        public void Actions_MoreThan4_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The maximum number of actions is 4.", () =>
            {
                template.Actions = new ITemplateAction[]
                {
                    new PostbackAction(),
                    new PostbackAction(),
                    new PostbackAction(),
                    new PostbackAction(),
                    new PostbackAction()
                };
            });
        }

        [TestMethod]
        public void Actions_4_ThrowsNoException()
        {
            ButtonsTemplate template = new ButtonsTemplate()
            {
                Actions = new ITemplateAction[]
                {
                    new PostbackAction(),
                    new MessageAction(),
                    new UriAction(),
                    new PostbackAction()
                }
            };
        }

        [TestMethod]
        public void Actions_InvalidTemplateActionType_ThrowsException()
        {
            ButtonsTemplate template = new ButtonsTemplate();

            ExceptionAssert.Throws<InvalidOperationException>("The template action type is invalid. Supported types are: IPostbackAction, IMessageAction and IUriAction.", () =>
            {
                template.Actions = new ITemplateAction[] { new TestTemplateAction() };
            });
        }
    }
}
