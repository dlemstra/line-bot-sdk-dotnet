// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class IActionConverterTests
    {
        public class TheCanReadProperty
        {
            [Fact]
            public void ShouldReturnTrue()
            {
                var converter = new IActionConverter();

                Assert.True(converter.CanRead);
            }
        }
    }
}
