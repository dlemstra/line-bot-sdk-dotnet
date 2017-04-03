// <copyright file="TestHttpClient.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.Net.Http;

namespace Line.Tests
{
    public sealed class TestHttpClient : HttpClient
    {
        private static readonly Uri TestBaseAddress = new Uri("http://line.dotnet/");

        private readonly TestHttpMessageHandler _handler;

        private TestHttpClient(TestHttpMessageHandler handler)
            : base(handler)
        {
            _handler = handler;

            BaseAddress = TestBaseAddress;
        }

        public HttpMethod RequestMethod
        {
            get
            {
                return _handler.Request.Method;
            }
        }

        public string RequestPath
        {
            get
            {
                return _handler.Request.RequestUri.ToString().Substring(TestBaseAddress.ToString().Length);
            }
        }

        public static TestHttpClient Create(string responseFile)
        {
            return new TestHttpClient(new TestHttpMessageHandler(responseFile));
        }
    }
}
