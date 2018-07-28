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
    public partial class CarouselColumnTests
    {
        [TestClass]
        public class TheTextProperty
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenSetToNull()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    column.Text = null;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be null or whitespace.", () =>
                {
                    column.Text = string.Empty;
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan120Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 120 characters.", () =>
                {
                    column.Text = new string('x', 121);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs120Chars()
            {
                var value = new string('x', 120);

                var column = new CarouselColumn()
                {
                    Text = value
                };

                Assert.AreEqual(value, column.Text);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenThumbnailUrlSetAndValueIsMoreThan60Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    column.ThumbnailUrl = new Uri("https://foo.bar/");
                    column.Text = new string('x', 61);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenThumbnailUrlSetAndValueIs60Chars()
            {
                var column = new CarouselColumn()
                {
                    ThumbnailUrl = new Uri("https://foo.bar/"),
                    Text = new string('x', 60)
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenTitleSetAndValueIsMoreThan60Chars()
            {
                var column = new CarouselColumn();

                ExceptionAssert.Throws<InvalidOperationException>("The text cannot be longer than 60 characters when the thumbnail url or title are set.", () =>
                {
                    column.Title = "Test";
                    column.Text = new string('x', 61);
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenTitleSetAndValueIs60Chars()
            {
                var template = new CarouselColumn()
                {
                    Title = "Test",
                    Text = new string('x', 60)
                };
            }
        }
    }
}
