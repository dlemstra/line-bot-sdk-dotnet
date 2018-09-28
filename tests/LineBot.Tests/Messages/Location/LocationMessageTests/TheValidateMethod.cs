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
    public partial class LocationMessageTests
    {
        [TestClass]
        public class TheValidateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenTitleIsNull()
            {
                ISendMessage message = new LocationMessage()
                {
                    Address = "Address"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The title cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenAddressIsNull()
            {
                ISendMessage message = new LocationMessage()
                {
                    Title = "Title"
                };

                ExceptionAssert.Throws<InvalidOperationException>("The address cannot be null.", () =>
                {
                    message.Validate();
                });
            }

            [TestMethod]
            public void ShouldNotThrowExceptionWhenValid()
            {
                ISendMessage message = new LocationMessage()
                {
                    Address = "Address",
                    Title = "Title"
                };

                message.Validate();
            }
        }
    }
}
