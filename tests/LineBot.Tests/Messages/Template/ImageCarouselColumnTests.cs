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
    public class ImageCarouselColumnTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            ImageCarouselColumn column = new ImageCarouselColumn
            {
                ImageUrl = new Uri("https://foo.bar"),
                Action = new UriAction()
            };

            string serialized = JsonConvert.SerializeObject(column);
            Assert.AreEqual(@"{""imageUrl"":""https://foo.bar"",""action"":{""type"":""uri"",""label"":null,""uri"":null}}", serialized);
        }

        [TestMethod]
        public void ImageUrl_SetToNull_ThrowsNoException()
        {
            ImageCarouselColumn column = new ImageCarouselColumn
            {
                ImageUrl = null
            };
        }

        [TestMethod]
        public void ImageUrl_NotHttps_ThrowsException()
        {
            ImageCarouselColumn column = new ImageCarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url should use the https scheme.", () =>
            {
                column.ImageUrl = new Uri("http://foo.bar");
            });
        }

        [TestMethod]
        public void ImageUrl_MoreThan1000Chars_ThrowsException()
        {
            ImageCarouselColumn column = new ImageCarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url cannot be longer than 1000 characters.", () =>
            {
                column.ImageUrl = new Uri("https://foo.bar/" + new string('x', 985));
            });
        }

        [TestMethod]
        public void ImageUrl_1000Chars_ThrowsNoException()
        {
            Uri value = new Uri("https://foo.bar/" + new string('x', 984));

            ImageCarouselColumn column = new ImageCarouselColumn()
            {
                ImageUrl = value
            };

            Assert.AreEqual(value, column.ImageUrl);
        }

        [TestMethod]
        public void Action_Null_ThrowsException()
        {
            ImageCarouselColumn column = new ImageCarouselColumn();

            ExceptionAssert.Throws<InvalidOperationException>("The action cannot be null.", () =>
            {
                column.Action = null;
            });
        }

        [TestMethod]
        public void Actions_InvalidTemplateActionType_ThrowsException()
        {
            ImageCarouselColumn column = new ImageCarouselColumn();

            ExceptionAssert.Throws<NotSupportedException>("The template action type is invalid. Supported types are: IPostbackAction, IMessageAction and IUriAction.", () =>
            {
                column.Action = new TestTemplateAction();
            });
        }
    }
}
