// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class UnixDateTimeConverterTests
    {
        [TestClass]
        public class TheCanReadProperty
        {
            [TestMethod]
            public void ShouldReturnTrue()
            {
                UnixDateTimeConverter converter = new UnixDateTimeConverter();

                Assert.IsTrue(converter.CanRead);
            }
        }
    }
}
