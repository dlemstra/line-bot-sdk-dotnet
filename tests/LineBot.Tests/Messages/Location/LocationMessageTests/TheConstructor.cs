// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LocationMessageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var message = new LocationMessage()
                {
                    Title = "Correct",
                    Address = "Somewhere",
                    Latitude = 13.4484625m,
                    Longitude = 144.7562962m
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.AreEqual(@"{""type"":""location"",""title"":""Correct"",""address"":""Somewhere"",""latitude"":13.4484625,""longitude"":144.7562962}", serialized);
            }
        }
    }
}