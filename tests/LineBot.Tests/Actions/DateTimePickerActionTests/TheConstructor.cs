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
    public partial class DateTimePickerActionTest
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var initial = new DateTime(2018, 10, 8, 10, 30, 0);
                var min = new DateTime(2018, 10, 8, 10, 00, 0);
                var max = new DateTime(2018, 10, 8, 11, 00, 0);

                var action = new DateTimePickerAction(DateTimePickerMode.DateTime)
                {
                    Label = "Foo",
                    Data = "Bar",
                    Initial = initial,
                    Min = min,
                    Max = max
                };
                string serialized = JsonConvert.SerializeObject(action);
                Assert.AreEqual(@"{""type"":""datetimepicker"",""mode"":""datetime"",""data"":""Bar"",""label"":""Foo"",""initial"":""2018-10-08T10:30"",""max"":""2018-10-08T11:00"",""min"":""2018-10-08T10:00""}", serialized);
            }
        }
    }
}