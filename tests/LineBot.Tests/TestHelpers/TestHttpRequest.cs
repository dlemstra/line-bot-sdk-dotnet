// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Line.Tests
{
    public sealed class TestHttpRequest : HttpRequest, IDisposable
    {
        public TestHttpRequest()
        {
            Body = new MemoryStream();
            Headers = new TestHeaderDictionary();
        }

        public TestHttpRequest(string requestFile)
        {
            Body = File.OpenRead(requestFile);
            Headers = CreateHeaders();
        }

        public override Stream Body { get; set; }

        public override long? ContentLength { get; set; }

        public override string ContentType { get; set; }

        public override IRequestCookieCollection Cookies { get; set; }

        public override IFormCollection Form { get; set; }

        public override bool HasFormContentType => false;

        public override IHeaderDictionary Headers { get; }

        public override HostString Host { get; set; }

        public override HttpContext HttpContext { get; }

        public override bool IsHttps { get; set; }

        public override string Method { get; set; }

        public override PathString Path { get; set; }

        public override PathString PathBase { get; set; }

        public override string Protocol { get; set; }

        public override IQueryCollection Query { get; set; }

        public override QueryString QueryString { get; set; }

        public override string Scheme { get; set; }

        public void Dispose()
        {
            Body.Dispose();
        }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult((IFormCollection)null);
        }

        private IHeaderDictionary CreateHeaders()
        {
            TestHeaderDictionary headers = new TestHeaderDictionary();

            byte[] content = Body.ToArrayAsync().Result;

            byte[] key = Encoding.UTF8.GetBytes(TestConfiguration.Create().ChannelSecret);

            using (HMACSHA256 hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(content);

                headers.Add("X-Line-Signature", new StringValues(Convert.ToBase64String(hash)));
            }

            Body.Position = 0;

            return headers;
        }
    }
}
