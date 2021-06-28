// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class ConfirmTemplateTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldCreateSerializeableObject()
            {
                var template = new ConfirmTemplate
                {
                    Text = "Test",
                    OkAction = new MessageAction()
                    {
                        Label = "OkLabel",
                        Text = "OkText"
                    },
                    CancelAction = new MessageAction()
                    {
                        Label = "CancelLabel",
                        Text = "CancelText"
                    }
                };

                var serialized = JsonSerializer.SerializeObject(template);
                Assert.AreEqual(@"{""type"":""confirm"",""text"":""Test"",""actions"":[{""type"":""message"",""label"":""OkLabel"",""text"":""OkText""},{""type"":""message"",""label"":""CancelLabel"",""text"":""CancelText""}]}", serialized);
            }
        }
    }
}
