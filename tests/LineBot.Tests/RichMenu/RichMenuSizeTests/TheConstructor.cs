// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class RichMenuSizeTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetTheProperties()
            {
                var size = new RichMenuSize(1686);

                Assert.Equal(1686, size.Height);
            }
        }
    }
}
