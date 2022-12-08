// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class CameraActionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldCreateSerializeableObject()
            {
                var action = new CameraAction
                {
                    Label = "Test"
                };

                var serialized = JsonSerializer.SerializeObject(action);
                Assert.Equal(@"{""type"":""camera"",""label"":""Test""}", serialized);
            }
        }
    }
}
