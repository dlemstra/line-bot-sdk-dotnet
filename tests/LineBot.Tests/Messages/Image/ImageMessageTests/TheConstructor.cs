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
    public partial class ImageMessageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateCreateSerializeableObject()
            {
                var message = new ImageMessage()
                {
                    Url = new Uri("https://foo.url"),
                    PreviewUrl = new Uri("https://foo.previewUrl"),
                };

                string serialized = JsonConvert.SerializeObject(message);
                Assert.AreEqual(@"{""type"":""image"",""originalContentUrl"":""https://foo.url"",""previewImageUrl"":""https://foo.previewUrl""}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var message = new ImageMessage(new Uri("https://foo.url"), new Uri("https://foo.previewUrl"));

                Assert.AreEqual("https://foo.url/", message.Url.ToString());
                Assert.AreEqual("https://foo.previewurl/", message.PreviewUrl.ToString());
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