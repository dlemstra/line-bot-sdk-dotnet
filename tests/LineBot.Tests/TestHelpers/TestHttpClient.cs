// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Line.Tests
{
    public sealed class TestHttpClient : HttpClient
    {
        private static readonly Uri TestBaseAddress = new Uri("http://line.dotnet/");

        private readonly TestHttpMessageHandler _handler;

        public TestHttpClient(TestHttpMessageHandler handler)
            : base(handler)
        {
            _handler = handler;

            BaseAddress = TestBaseAddress;
        }

        public string PostedData => _handler.Requests.LastOrDefault()?.GetPostedData();

        public IEnumerable<HttpRequestMessage> Requests => _handler.Requests;

        public HttpMethod RequestMethod => _handler.Requests.LastOrDefault()?.Method;

        public string RequestPath => _handler.Requests.LastOrDefault()?.RequestUri.PathAndQuery;

        public static TestHttpClient Create()
        {
            return new TestHttpClient(new TestHttpMessageHandler(HttpStatusCode.OK));
        }

        public static TestHttpClient Create(string responseFile)
        {
            return new TestHttpClient(new TestHttpMessageHandler(responseFile));
        }

        public static TestHttpClient ThatReturnsAnError()
        {
            return new TestHttpClient(new TestHttpMessageHandler(HttpStatusCode.InternalServerError));
        }

        public static TestHttpClient ThatReturnsData(byte[] data)
        {
            return new TestHttpClient(new TestHttpMessageHandler(data));
        }
    }
}
