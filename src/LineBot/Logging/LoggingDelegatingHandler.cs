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

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Line
{
    internal sealed class LoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILineBotLogger _logger;

        public LoggingDelegatingHandler(ILineBotLogger logger)
            : this(logger, new HttpClientHandler())
        {
        }

        internal LoggingDelegatingHandler(ILineBotLogger logger, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                await _logger.LogApiCall(request.RequestUri, request.Content).ConfigureAwait(false);
            }
        }
    }
}
