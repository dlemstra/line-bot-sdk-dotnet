// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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
