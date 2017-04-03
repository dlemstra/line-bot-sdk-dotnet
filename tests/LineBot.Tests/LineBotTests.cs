// <copyright file="LineBotTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class LineBotTests
    {
        [TestMethod]
        public void Create_ConfigurationIsNull_ThrowsArgumentNullException()
        {
            ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
            {
                new LineBot(null);
            });
        }
    }
}
