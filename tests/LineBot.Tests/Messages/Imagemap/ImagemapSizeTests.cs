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
    public class ImagemapSizeTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            ImagemapSize size = new ImagemapSize()
            {
                Width = 10,
                Height = 20
            };

            string serialized = JsonConvert.SerializeObject(size);
            Assert.AreEqual(@"{""width"":10,""height"":20}", serialized);
        }

        [TestMethod]
        public void Width_Zero_ThrowsException()
        {
            ImagemapSize area = new ImagemapSize();

            ExceptionAssert.Throws<InvalidOperationException>("The width should be at least 1.", () =>
            {
                area.Width = 0;
            });
        }

        [TestMethod]
        public void Height_Zero_ThrowsException()
        {
            ImagemapSize area = new ImagemapSize();

            ExceptionAssert.Throws<InvalidOperationException>("The height should be at least 1.", () =>
            {
                area.Height = 0;
            });
        }
    }
}
