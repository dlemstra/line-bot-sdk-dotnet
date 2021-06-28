// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Line.Tests
{
    public sealed class TestLogger : ILineBotLogger
    {
        public byte[] LogReceivedEventsEventsData { get; private set; }

        public Uri LogApiCallUri { get; private set; }

        public HttpContent LogApiCallHttpContent { get; private set; }

        public Task LogApiCall(Uri uri, HttpContent httpContent)
        {
            LogApiCallUri = uri;
            LogApiCallHttpContent = httpContent;

            return Task.CompletedTask;
        }

        public Task LogReceivedEvents(byte[] eventsData)
        {
            LogReceivedEventsEventsData = eventsData;

            return Task.CompletedTask;
        }
    }
}
