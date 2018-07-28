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
    public partial class ButtonsTemplateTests
    {
        [TestClass]
        public class TheThumbnailUrlProperty
        {
            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIsNull()
            {
                ButtonsTemplate template = new ButtonsTemplate
                {
                    ThumbnailUrl = null
                };
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNotHttps()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url should use the https scheme.", () =>
                {
                    template.ThumbnailUrl = new Uri("http://foo.bar");
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsMoreThan1000Chars()
            {
                ButtonsTemplate template = new ButtonsTemplate();

                ExceptionAssert.Throws<InvalidOperationException>("The thumbnail url cannot be longer than 1000 characters.", () =>
                {
                    template.ThumbnailUrl = new Uri("https://foo.bar/" + new string('x', 985));
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValueIs1000Chars()
            {
                Uri value = new Uri("https://foo.bar/" + new string('x', 984));

                ButtonsTemplate template = new ButtonsTemplate()
                {
                    ThumbnailUrl = value
                };

                Assert.AreEqual(value, template.ThumbnailUrl);
            }
        }
    }
}
