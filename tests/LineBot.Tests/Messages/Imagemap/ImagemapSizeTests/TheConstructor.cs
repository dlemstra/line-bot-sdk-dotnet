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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    public partial class ImagemapSizeTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var size = new ImagemapSize()
                {
                    Width = 10,
                    Height = 20
                };

                var serialized = JsonConvert.SerializeObject(size);
                Assert.AreEqual(@"{""width"":10,""height"":20}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var message = new ImagemapSize(100, 200);

                Assert.AreEqual(100, message.Width);
                Assert.AreEqual(200, message.Height);
            }
        }
    }
}
