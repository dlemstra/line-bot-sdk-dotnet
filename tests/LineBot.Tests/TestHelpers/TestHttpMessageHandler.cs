// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Line.Tests
{
    [ExcludeFromCodeCoverage]
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

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

        public HttpRequestMessage Request { get; set; }

        public string PostedData { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;

            StringContent content = request.Content as StringContent;
            if (content != null)
                PostedData = await content.ReadAsStringAsync();

            return await Task.FromResult(_response);
        }
    }
}
