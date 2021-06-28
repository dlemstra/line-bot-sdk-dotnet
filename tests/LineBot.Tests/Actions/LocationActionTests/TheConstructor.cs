// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class LocationActionTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var action = new CameraRollAction
                {
                    Label = "Test"
                };

                string serialized = JsonSerializer.SerializeObject(action);
                Assert.AreEqual(@"{""type"":""cameraRoll"",""label"":""Test""}", serialized);
            }
        }
    }
}