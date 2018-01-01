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
    public class UriActionTests
    {
        [TestMethod]
        public void Constructor_SerializedCorrectly()
        {
            UriAction action = new UriAction
            {
                Label = "Foo",
                Url = new Uri("http://foo.bar")
            };

            string serialized = JsonConvert.SerializeObject(action);
            Assert.AreEqual(@"{""type"":""uri"",""label"":""Foo"",""uri"":""http://foo.bar""}", serialized);
        }

        [TestMethod]
        public void Label_Null_ThrowsException()
        {
            UriAction action = new UriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
            {
                action.Label = null;
            });
        }

        [TestMethod]
        public void Label_Empty_ThrowsException()
        {
            UriAction action = new UriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be null or whitespace.", () =>
            {
                action.Label = string.Empty;
            });
        }

        [TestMethod]
        public void Label_MoreThan20Chars_ThrowsException()
        {
            UriAction action = new UriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The label cannot be longer than 20 characters.", () =>
            {
                action.Label = new string('x', 21);
            });
        }

        [TestMethod]
        public void Label_20Chars_ThrowsNoException()
        {
            string value = new string('x', 20);

            UriAction action = new UriAction()
            {
                Label = value
            };

            Assert.AreEqual(value, action.Label);
        }

        [TestMethod]
        public void Url_Null_ThrowsException()
        {
            UriAction action = new UriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
            {
                action.Url = null;
            });
        }

        [TestMethod]
        public void Url_NotHttpOrHttpsOrTel_ThrowsException()
        {
            UriAction action = new UriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The url should use the http, https or tel scheme.", () =>
            {
                action.Url = new Uri("ftp://foo.bar");
            });
        }

        [TestMethod]
        public void Url_Tel_ThrowsNoException()
        {
            UriAction action = new UriAction()
            {
                Url = new Uri("tel://1234")
            };
        }

        [TestMethod]
        public void Url_MoreThan1000Chars_ThrowsException()
        {
            UriAction action = new UriAction();

            ExceptionAssert.Throws<InvalidOperationException>("The url cannot be longer than 1000 characters.", () =>
            {
                action.Url = new Uri("https://foo.bar/" + new string('x', 985));
            });
        }

        [TestMethod]
        public void Url_1000Chars_ThrowsNoException()
        {
            Uri value = new Uri("http://foo.bar/" + new string('x', 984));

            UriAction action = new UriAction()
            {
                Url = value
            };

            Assert.AreEqual(value, action.Url);
        }
    }
}
