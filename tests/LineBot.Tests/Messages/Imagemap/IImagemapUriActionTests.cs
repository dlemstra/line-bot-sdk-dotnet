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
    public class IImagemapUriActionTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            ImagemapUriAction action = new ImagemapUriAction()
            {
                Url = new Uri("https://foo.bar"),
                Area = new ImagemapArea(0, 10, 20, 30)
            };

            string serialized = JsonConvert.SerializeObject(action);
            Assert.AreEqual(@"{""type"":""uri"",""linkUri"":""https://foo.bar"",""area"":{""x"":0,""y"":10,""width"":20,""height"":30}}", serialized);
        }

        [TestMethod]
        public void Url_Null_ThrowsException()
        {
            ImagemapUriAction action = new ImagemapUriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                action.Url = null;
            });
        }

        [TestMethod]
        public void Url_MoreThan1000Chars_ThrowsException()
        {
            ImagemapUriAction action = new ImagemapUriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 1000 characters.", () =>
            {
                action.Url = new Uri("https://foo.bar/" + new string('x', 985));
            });
        }

        [TestMethod]
        public void Url_1000Chars_ThrowsNoException()
        {
            Uri value = new Uri("https://foo.bar/" + new string('x', 984));

            ImagemapUriAction action = new ImagemapUriAction()
            {
                Url = value
            };

            Assert.AreEqual(value, action.Url);
        }

        [TestMethod]
        public void Area_Null_ThrowsException()
        {
            ImagemapUriAction action = new ImagemapUriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The area cannot be null.", () =>
            {
                action.Area = null;
            });
        }
    }
}
