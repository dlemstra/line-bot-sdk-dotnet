// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class PostbackActionTest
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var action = new PostbackAction
                {
                    Label = "Foo",
                    Data = "Bar",
                    Text = "Test"
                };

                var serialized = JsonSerializer.SerializeObject(action);
                Assert.AreEqual(@"{""type"":""postback"",""label"":""Foo"",""data"":""Bar"",""text"":""Test""}", serialized);
            }
        }
    }
}