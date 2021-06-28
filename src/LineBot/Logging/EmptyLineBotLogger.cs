// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Line
{
    internal sealed class EmptyLineBotLogger : ILineBotLogger
    {
        public Task LogReceivedEvents(byte[] eventsData)
        {
            return Task.CompletedTask;
        }

        public Task LogApiCall(Uri uri, HttpContent httpContent)
        {
            return Task.CompletedTask;
        }
    }
}
