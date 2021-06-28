// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        [TestClass]
        public class UnixDateTimeConverterTests
        {
            [TestMethod]
            public void ShouldThrowException()
            {
                UnixDateTimeConverter converter = new UnixDateTimeConverter();

                ExceptionAssert.Throws<NotSupportedException>(() =>
                {
                    converter.WriteJson(null, null, null);
                });
            }
        }
    }
}
