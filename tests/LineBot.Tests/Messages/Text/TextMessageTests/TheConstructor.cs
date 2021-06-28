// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TextMessageTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var message = new TextMessage()
                {
                    Text = "Correct"
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.AreEqual(@"{""type"":""text"",""text"":""Correct""}", serialized);
            }

            [TestMethod]
            public void ShouldSetTheProperties()
            {
                var message = new TextMessage("Correct");

                Assert.AreEqual("Correct", message.Text);
            }
        }
    }
}
