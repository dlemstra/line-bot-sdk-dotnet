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
    public partial class ImagemapMessageActionTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var action = new ImagemapMessageAction()
                {
                    Text = "Correct",
                    Area = new ImagemapArea(0, 10, 20, 30)
                };

                var serialized = JsonConvert.SerializeObject(action);
                Assert.AreEqual(@"{""type"":""message"",""text"":""Correct"",""area"":{""x"":0,""y"":10,""width"":20,""height"":30}}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var action = new ImagemapMessageAction("test", new ImagemapArea(1, 2, 3, 4));

                Assert.IsNotNull(action.Area);
                Assert.AreEqual("test", action.Text);
                Assert.AreEqual(1, action.Area.X);
                Assert.AreEqual(2, action.Area.Y);
                Assert.AreEqual(3, action.Area.Width);
                Assert.AreEqual(4, action.Area.Height);
            }

            [TestMethod]
            public void ShouldSetTheArea()
            {
                var action = new ImagemapMessageAction("test", 1, 2, 3, 4);

                Assert.IsNotNull(action.Area);
                Assert.AreEqual("test", action.Text);
                Assert.AreEqual(1, action.Area.X);
                Assert.AreEqual(2, action.Area.Y);
                Assert.AreEqual(3, action.Area.Width);
                Assert.AreEqual(4, action.Area.Height);
            }

            [TestMethod]
            public void ShouldConvertStringUrl()
            {
                var message = new ImageMessage("https://foo.url", "https://foo.previewUrl");

                Assert.AreEqual("https://foo.url/", message.Url.ToString());
                Assert.AreEqual("https://foo.previewurl/", message.PreviewUrl.ToString());
            }
        }
    }
}