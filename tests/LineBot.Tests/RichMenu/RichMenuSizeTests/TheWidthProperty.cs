// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class TheRichMenuSizeTests
    {
        public partial class TheWidthProperty
        {
            [Fact]
            public void ShouldReturn2500()
            {
                var richMenuSize = new RichMenuSize();

                Assert.Equal(2500, richMenuSize.Width);
            }
        }
    }
}
