// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Xunit;

namespace Line.Tests
{
    public partial class IActionConverterTests
    {
        public class TheCanConvertMethod
        {
            [Fact]
            public void ShouldReturnTrueWhenTypeIsIAction()
            {
                var converter = new IActionConverter();

                Assert.True(converter.CanConvert(typeof(IAction)));
            }

            [Fact]
            public void ShouldReturnTrueWhenTypeImplementsIAction()
            {
                var converter = new IActionConverter();

                Assert.True(converter.CanConvert(typeof(TestAction)));
            }
        }
    }
}
