// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class ConfirmTemplateTests
    {
        public class TheConstructor
        {
            [Fact]
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
                Assert.Equal(@"{""type"":""confirm"",""text"":""Test"",""actions"":[{""type"":""message"",""label"":""OkLabel"",""text"":""OkText""},{""type"":""message"",""label"":""CancelLabel"",""text"":""CancelText""}]}", serialized);
            }
        }
    }
}
