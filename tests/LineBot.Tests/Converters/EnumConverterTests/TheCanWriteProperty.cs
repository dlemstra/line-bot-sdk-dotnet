// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        [TestClass]
        public class TheCanWriteProperty
        {
            [TestMethod]
            public void ShouldReturnTrue()
            {
                EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

                Assert.IsTrue(converter.CanWrite);
            }
        }
    }
}
