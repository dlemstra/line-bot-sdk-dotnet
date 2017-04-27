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
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class LineConfigurationTests
    {
        [TestMethod]
        public void Create_ChannelAccessTokenIsNull_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = null,
                ChannelSecret = "ChannelSecret",
            };

            ExceptionAssert.Throws<LineBotException>("ChannelAccessToken cannot be null or whitespace.", () =>
            {
                configuration.CreateBot();
            });
        }

        [TestMethod]
        public void Create_ChannelAccessTokenIsEmpty_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = string.Empty,
                ChannelSecret = "ChannelSecret",
            };

            ExceptionAssert.Throws<LineBotException>("ChannelAccessToken cannot be null or whitespace.", () =>
            {
                configuration.CreateBot();
            });
        }

        [TestMethod]
        public void Create_ChannelAccessTokenIsWhitespace_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = "  ",
                ChannelSecret = "ChannelSecret",
            };

            ExceptionAssert.Throws<LineBotException>("ChannelAccessToken cannot be null or whitespace.", () =>
            {
                configuration.CreateBot();
            });
        }

        [TestMethod]
        public void Create_ChannelSecretTokenIsNull_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = "ChannelAccessToken",
                ChannelSecret = null,
            };

            ExceptionAssert.Throws<LineBotException>("ChannelSecret cannot be null or whitespace.", () =>
            {
                configuration.CreateBot();
            });
        }

        [TestMethod]
        public void Create_ChannelSecretTokenIsEmpty_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = "ChannelAccessToken",
                ChannelSecret = string.Empty,
            };

            ExceptionAssert.Throws<LineBotException>("ChannelSecret cannot be null or whitespace.", () =>
            {
                configuration.CreateBot();
            });
        }

        [TestMethod]
        public void Create_ChannelSecretTokenIsWhitespace_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = "ChannelAccessToken",
                ChannelSecret = "  ",
            };

            ExceptionAssert.Throws<LineBotException>("ChannelSecret cannot be null or whitespace.", () =>
            {
                configuration.CreateBot();
            });
        }

        [TestMethod]
        public void Create_HttpFactoryIsUsed_BaseAddressUsesApi()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = "ChannelAccessToken",
                ChannelSecret = "ChannelSecret",
            };

            ILineBot bot = configuration.CreateBot();

            FieldInfo field = bot.GetType().GetRuntimeFields().Where(f => f.Name == "_client").First();
            HttpClient client = (HttpClient)field.GetValue(bot);

            Assert.AreEqual(new Uri("https://api.line.me/v2/bot/"), client.BaseAddress);
        }
    }
}
