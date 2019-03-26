// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
    public partial class VideoMessageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var message = new VideoMessage()
                {
                    Url = new Uri("https://foo.url"),
                    PreviewUrl = new Uri("https://foo.previewUrl"),
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.AreEqual(@"{""type"":""video"",""originalContentUrl"":""https://foo.url"",""previewImageUrl"":""https://foo.previewUrl""}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var message = new VideoMessage(new Uri("https://foo.url"), new Uri("https://foo.previewUrl"));

                Assert.AreEqual("https://foo.url/", message.Url.ToString());
                Assert.AreEqual("https://foo.previewurl/", message.PreviewUrl.ToString());
            }

            [TestMethod]
            public void ShouldConvertStringUrl()
            {
                var message = new VideoMessage("https://foo.url", "https://foo.previewUrl");

                Assert.AreEqual("https://foo.url/", message.Url.ToString());
                Assert.AreEqual("https://foo.previewurl/", message.PreviewUrl.ToString());
            }
        }
    }
}