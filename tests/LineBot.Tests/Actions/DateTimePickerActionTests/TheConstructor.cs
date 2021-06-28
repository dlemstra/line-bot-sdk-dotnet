// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                string serialized = JsonSerializer.SerializeObject(action);
                Assert.AreEqual(@"{""mode"":""datetime"",""type"":""datetimepicker"",""data"":""Bar"",""label"":""Foo"",""initial"":""2018-10-08T10:30"",""max"":""2018-10-08T11:00"",""min"":""2018-10-08T10:00""}", serialized);
            }
        }
    }
}