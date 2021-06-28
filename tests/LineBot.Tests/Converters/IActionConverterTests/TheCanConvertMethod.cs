// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class IActionConverterTests
    {
        [TestClass]
        public class TheCanConvertMethod
        {
            [TestMethod]
            public void ShouldReturnTrueWhenTypeIsIAction()
            {
                var converter = new IActionConverter();

                Assert.IsTrue(converter.CanConvert(typeof(IAction)));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenTypeImplementsIAction()
            {
                var converter = new IActionConverter();

                Assert.IsTrue(converter.CanConvert(typeof(TestAction)));
            }
        }
    }
}