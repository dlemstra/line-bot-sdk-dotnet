// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Line.Tests
{
    public partial class EnumConverterTests
    {
        [TestClass]
        public class TheWriteJsonMethod
        {
            [TestMethod]
            public void ShouldWriteValueInLowerCase()
            {
                EnumConverter<TestEnum> converter = new EnumConverter<TestEnum>();

                string value = JsonConvert.SerializeObject(TestEnum.Test, converter);
                Assert.AreEqual(@"""test""", value);
            }
        }
    }
}
