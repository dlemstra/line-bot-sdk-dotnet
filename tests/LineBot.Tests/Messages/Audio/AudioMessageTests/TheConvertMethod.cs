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
    public partial class AudioMessageTests
    {
        [TestClass]
        public class TheConvertMethod
        {
            [TestMethod]
            public void ShouldPreserveInstanceWhenValueIsAudioMessage()
            {
                var message = new AudioMessage()
                {
                    Url = new Uri("https://foo.url"),
                    Duration = 10000
                };

                var audioMessage = AudioMessage.Convert(message);

                Assert.AreSame(message, audioMessage);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenUrlIsNull()
            {
                var message = new AudioMessage()
                {
                    Duration = 10000
                };

                ExceptionAssert.Throws<InvalidOperationException>("The url cannot be null.", () =>
                {
                    AudioMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenDurationIsZero()
            {
                var message = new AudioMessage()
                {
                    Url = new Uri("https://foo.url")
                };

                ExceptionAssert.Throws<InvalidOperationException>("The duration should be at least 1 millisecond.", () =>
                {
                    AudioMessage.Convert(message);
                });
            }

            [TestMethod]
            public void ShouldConvertCustomIAudioMessageToAudioMessage()
            {
                var message = new TestAudioMessage();

                var audioMessage = AudioMessage.Convert(message);

                Assert.AreNotEqual(message, audioMessage);
                Assert.AreEqual(new Uri("https://foo.url"), audioMessage.Url);
                Assert.AreEqual(1000, audioMessage.Duration);
            }
        }
    }
}
