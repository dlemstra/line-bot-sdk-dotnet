// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class TheRichMenuSizeTests
    {
        [TestClass]
        public partial class TheWidthProperty
        {
            [TestMethod]
            public void ShouldReturn2500()
            {
                var richMenuSize = new RichMenuSize();

                Assert.AreEqual(2500, richMenuSize.Width);
            }
        }
    }
}