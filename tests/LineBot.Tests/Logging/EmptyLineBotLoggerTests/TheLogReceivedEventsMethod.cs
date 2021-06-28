// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class EmptyLineBotLoggerTests
    {
        [TestClass]
        public class TheLogReceivedEventsMethod
        {
            [TestMethod]
            public async Task ShouldDoNothing()
            {
                var logger = new EmptyLineBotLogger();
                await logger.LogReceivedEvents(null);
            }
        }
    }
}
