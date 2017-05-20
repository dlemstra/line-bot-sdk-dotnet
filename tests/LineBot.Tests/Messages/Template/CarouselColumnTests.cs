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
    public class CarouselColumnTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            CarouselColumn column = new CarouselColumn
            {
                ThumbnailUrl = new Uri("https://foo.bar"),
                Title = "Foo",
                Text = "Test"
            };

            string serialized = JsonConvert.SerializeObject(column);
            Assert.AreEqual(@"{""thumbnailImageUrl"":""https://foo.bar"",""title"":""Foo"",""text"":""Test"",""actions"":null}", serialized);
        }

        [TestMethod]
        public void ThumbnailUrl_SetToNull_ThrowsNoException()
        {
            CarouselColumn column = new CarouselColumn
            {
                ThumbnailUrl = null
            };
        }

        [TestMethod]
        public void ThumbnailUrl_NotHttps_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url should use the https scheme.", () =>
            {
                column.ThumbnailUrl = new Uri("http://foo.bar");
            });
        }

        [TestMethod]
        public void ThumbnailUrl_MoreThan1000Chars_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url cannot be longer than 1000 characters.", () =>
            {
                column.ThumbnailUrl = new Uri("https://foo.bar/" + new string('x', 985));
            });
        }

        [TestMethod]
        public void ThumbnailUrl_1000Chars_ThrowsNoException()
        {
            Uri value = new Uri("https://foo.bar/" + new string('x', 984));

            CarouselColumn column = new CarouselColumn()
            {
                ThumbnailUrl = value
            };

            Assert.AreEqual(value, column.ThumbnailUrl);
        }

        [TestMethod]
        public void Title_SetToNull_ThrowsNoException()
        {
            CarouselColumn column = new CarouselColumn
            {
                Title = null
            };
        }

        [TestMethod]
        public void Title_MoreThan40Chars_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The title cannot be longer than 40 characters.", () =>
            {
                column.Title = new string('x', 41);
            });
        }

        [TestMethod]
        public void Title_40Chars_ThrowsNoException()
        {
            string value = new string('x', 40);

            CarouselColumn column = new CarouselColumn()
            {
                Title = value
            };

            Assert.AreEqual(value, column.Title);
        }

        [TestMethod]
        public void Text_Null_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                column.Text = null;
            });
        }

        [TestMethod]
        public void Text_Empty_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
            {
                column.Text = string.Empty;
            });
        }

        [TestMethod]
        public void Text_MoreThan120Chars_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 120 characters.", () =>
            {
                column.Text = new string('x', 121);
            });
        }

        [TestMethod]
        public void Text_120Chars_ThrowsNoException()
        {
            string value = new string('x', 120);

            CarouselColumn column = new CarouselColumn()
            {
                Text = value
            };

            Assert.AreEqual(value, column.Text);
        }

        [TestMethod]
        public void TextWithThumbnailUrlSet_MoreThan60Chars_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
            {
                column.ThumbnailUrl = new Uri("https://foo.bar/");
                column.Text = new string('x', 61);
            });
        }

        [TestMethod]
        public void TextWithThumbnailUrlSet_60Chars_ThrowsNoException()
        {
            CarouselColumn column = new CarouselColumn();

            column.ThumbnailUrl = new Uri("https://foo.bar/");
            column.Text = new string('x', 60);
        }

        [TestMethod]
        public void TextWithTitleSet_MoreThan60Chars_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
            {
                column.Title = "Test";
                column.Text = new string('x', 61);
            });
        }

        [TestMethod]
        public void TextWithTitleSet_60Chars_ThrowsNoException()
        {
            CarouselColumn column = new CarouselColumn();

            column.Title = "Test";
            column.Text = new string('x', 60);
        }

        [TestMethod]
        public void Actions_Null_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The actions cannot be null.", () =>
            {
                column.Actions = null;
            });
        }

        [TestMethod]
        public void Actions_LessThan1_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The minimum number of actions is 1.", () =>
            {
                column.Actions = new ITemplateAction[] { };
            });
        }

        [TestMethod]
        public void Actions_MoreThan3_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The maximum number of actions is 3.", () =>
            {
                column.Actions = new ITemplateAction[]
                {
                    new PostbackAction(),
                    new PostbackAction(),
                    new PostbackAction(),
                    new PostbackAction()
                };
            });
        }

        [TestMethod]
        public void Actions_3_ThrowsNoException()
        {
            CarouselColumn column = new CarouselColumn()
            {
                Actions = new ITemplateAction[]
                {
                    new PostbackAction(),
                    new MessageAction(),
                    new UriAction()
                }
            };
        }

        [TestMethod]
        public void Actions_InvalidTemplateActionType_ThrowsException()
        {
            CarouselColumn column = new CarouselColumn();

            ExceptionAssert.Throws<NotSupportedException>("The template action type is invalid. Supported types are: IPostbackAction, IMessageAction and IUriAction.", () =>
            {
                column.Actions = new ITemplateAction[] { new TestTemplateAction() };
            });
        }
    }
}
