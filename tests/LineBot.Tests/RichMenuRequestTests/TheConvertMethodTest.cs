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
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class TheConvertMethodTest
    {
        [TestMethod]
        public void ShouldPreserveInstanceWhenValueIsRichMenuRequest()
        {
            var richMenuRequest = new RichMenuRequest
            {
                ChatBarText = "testChatBarTxt",
                Name = "testName",
                Areas = new[]
                {
                    new RichMenuArea
                    {
                        Action = new UriAction { Label = "testLabel", Url = new Uri("http://www.google.com") },
                        RichMenuBounds = new RichMenuBounds
                        {
                            Width = 110,
                            Height = 120,
                            X = 11,
                            Y = 12
                        }
                    },
                    new RichMenuArea
                    {
                        Action = new UriAction { Label = "testLabel2", Url = new Uri("http://www.bing.com") },
                        RichMenuBounds = new RichMenuBounds
                        {
                            Width = 210,
                            Height = 220,
                            X = 21,
                            Y = 22
                        }
                    }
                },
                Size = new RichMenuSize { Height = 1686, Width = 2500 },
                Selected = false
            };
            var convertedRichMenuRequest = RichMenuRequest.Convert(richMenuRequest);

            Assert.AreEqual(richMenuRequest, convertedRichMenuRequest);
        }
    }
}