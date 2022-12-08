// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class PostbackActionTest
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var action = new PostbackAction
                {
                    Label = "Foo",
                    Data = "Bar",
                    Text = "Test"
                };

                var serialized = JsonSerializer.SerializeObject(action);
                Assert.Equal(@"{""type"":""postback"",""label"":""Foo"",""data"":""Bar"",""text"":""Test""}", serialized);
            }
        }
    }
}
