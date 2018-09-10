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
    public partial class ImagemapUriActionTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsImagemapUriAction()
            {
                var action = new ImagemapUriAction("http://foo.bar", 1, 2, 3, 4);

                var uriAction = ImagemapUriAction.Convert(action);

                Assert.AreSame(action, uriAction);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenUrlIsNull()
            {
                var action = new ImagemapUriAction()
                {
                    Area = new ImagemapArea(1, 2, 3, 4)
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    ImagemapUriAction.Convert(action);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAreaIsNull()
            {
                var action = new ImagemapUriAction()
                {
                    Url = new Uri("http://foo.bar")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
                {
                    ImagemapUriAction.Convert(action);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIImagemapMessageActionToImagemapMessageAction()
            {
                var action = new TestImagemapUriAction();

                var uriAction = ImagemapUriAction.Convert(action);

                Assert.AreNotEqual(action, uriAction);
                Assert.AreEqual("https://foo.bar/", uriAction.Url.ToString());
                Assert.IsNotNull(uriAction.Area);
                Assert.AreEqual(4, uriAction.Area.X);
                Assert.AreEqual(3, uriAction.Area.Y);
                Assert.AreEqual(2, uriAction.Area.Width);
                Assert.AreEqual(1, uriAction.Area.Height);
            }
        }
    }
}
