// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    public partial class HttpClientFactoryTests
    {
        [TestClass]
        public class TheCreateMethod
        {
            [TestMethod]
            public void ShouldReturnNewInstance()
            {
                LineConfiguration configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnNewInstance)
                };

                HttpClient clientA = HttpClientFactory.Create(configuration, null);
                HttpClient clientB = HttpClientFactory.Create(configuration, null);

                Assert.AreNotEqual(clientA, clientB);
            }

            [TestMethod]
            public void ShouldReturnInitializedHttpClient()
            {
                LineConfiguration configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnInitializedHttpClient)
                };

                HttpClient client = HttpClientFactory.Create(configuration, null);

                Assert.AreEqual(new Uri("https://api.line.me/v2/bot/"), client.BaseAddress);
                string authorization = client.DefaultRequestHeaders.GetValues("Authorization").First();
                Assert.AreEqual("Bearer " + nameof(ShouldReturnInitializedHttpClient), authorization);
            }

            [TestMethod]
            public void ShouldSetInnerHandlerToLineBotLogger()
            {
                LineConfiguration configuration = new LineConfiguration()
                {
                    ChannelAccessToken = nameof(ShouldReturnInitializedHttpClient)
                };

                HttpClient client = HttpClientFactory.Create(configuration, null);

                FieldInfo field = client.GetType().BaseType.GetRuntimeFields().Where(f => f.Name == "_handler").First();
                LoggingDelegatingHandler logger = field.GetValue(client) as LoggingDelegatingHandler;
                Assert.IsNotNull(logger);
                Assert.IsInstanceOfType(logger.InnerHandler, typeof(HttpClientHandler));
            }
        }
    }
}
