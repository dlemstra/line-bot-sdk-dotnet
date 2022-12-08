// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class TextMessageTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var message = new TextMessage()
                {
                    Text = "Correct"
                };

                var serialized = JsonSerializer.SerializeObject(message);
                Assert.Equal(@"{""type"":""text"",""text"":""Correct""}", serialized);
            }

            [Fact]
            public void ShouldSetTheProperties()
            {
                var message = new TextMessage("Correct");

                Assert.Equal("Correct", message.Text);
            }
        }
    }
}
