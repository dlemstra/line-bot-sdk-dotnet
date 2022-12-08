// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public partial class EmptyLineBotLoggerTests
    {
        public class TheLogApiCallMethod
        {
            [Fact]
            public async Task ShouldDoNothing()
            {
                var logger = new EmptyLineBotLogger();
                await logger.LogApiCall(null, null);
            }
        }
    }
}
