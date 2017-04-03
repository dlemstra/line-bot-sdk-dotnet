// <copyright file="TestHttpMessageHandler.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Line.Tests
{
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public TestHttpMessageHandler(string responseFile)
            : this(HttpStatusCode.OK, responseFile)
        {
        }

        public TestHttpMessageHandler(HttpStatusCode statusCode, string responseFile)
        {
            _response = new HttpResponseMessage()
            {
                StatusCode = statusCode,
                Content = new StringContent(File.ReadAllText(responseFile))
            };
        }

        public HttpRequestMessage Request { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;

            return Task.FromResult(_response);
        }
    }
}
