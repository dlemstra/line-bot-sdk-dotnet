// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
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
        private readonly List<HttpRequestMessage> _requests = new List<HttpRequestMessage>();

        public TestHttpMessageHandler(byte[] data)
            : this(HttpStatusCode.OK)
        {
            _response.Content = new ByteArrayContent(data);
        }

        public TestHttpMessageHandler(string responseFile)
            : this(HttpStatusCode.OK)
        {
            _response.Content = new StringContent(File.ReadAllText(responseFile));
        }

        public TestHttpMessageHandler(HttpStatusCode statusCode)
        {
            _response = new HttpResponseMessage()
            {
                StatusCode = statusCode,
            };
        }

        public IEnumerable<HttpRequestMessage> Requests => _requests;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetPostedData();

            _requests.Add(request);

            return await Task.FromResult(_response);
        }
    }
}
