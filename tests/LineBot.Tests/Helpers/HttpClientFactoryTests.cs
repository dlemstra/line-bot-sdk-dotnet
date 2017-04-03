// <copyright file="HttpClientFactoryTests.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.Linq;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class HttpClientFactoryTests
    {
        [TestMethod]
        public void Create_ConfigurationIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("configuration", () =>
            {
                HttpClientFactory.Create(null);
            });
        }

        [TestMethod]
        public void Create_ChannelAccessTokenIsNull_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration();

            ExceptionAssert.Throws<InvalidOperationException>("The ChannelAccessToken should not be null or whitespace.", () =>
            {
                HttpClientFactory.Create(configuration);
            });
        }

        [TestMethod]
        public void Create_ChannelAccessTokenIsEmpty_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = string.Empty
            };

            ExceptionAssert.Throws<InvalidOperationException>("The ChannelAccessToken should not be null or whitespace.", () =>
            {
                HttpClientFactory.Create(configuration);
            });
        }

        [TestMethod]
        public void Create_ChannelAccessTokenIsWhitespace_ThrowsException()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = " "
            };

            ExceptionAssert.Throws<InvalidOperationException>("The ChannelAccessToken should not be null or whitespace.", () =>
            {
                HttpClientFactory.Create(configuration);
            });
        }

        [TestMethod]
        public void Create_SameChannelAccessToken_ReturnsSameInstance()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = nameof(Create_SameChannelAccessToken_ReturnsSameInstance)
            };

            HttpClient clientA = HttpClientFactory.Create(configuration);
            HttpClient clientB = HttpClientFactory.Create(configuration);

            Assert.AreEqual(clientA, clientB);
        }

        [TestMethod]
        public void Create_DifferentChannelAccessToken_ReturnsDifferentInstance()
        {
            LineConfiguration configurationA = new LineConfiguration()
            {
                ChannelAccessToken = $"{nameof(Create_DifferentChannelAccessToken_ReturnsDifferentInstance)}A"
            };

            LineConfiguration configurationB = new LineConfiguration()
            {
                ChannelAccessToken = $"{nameof(Create_DifferentChannelAccessToken_ReturnsDifferentInstance)}B"
            };

            HttpClient clientA = HttpClientFactory.Create(configurationA);
            HttpClient clientB = HttpClientFactory.Create(configurationB);

            Assert.AreNotEqual(clientA, clientB);
        }

        [TestMethod]
        public void Create_InstanceIsInitialized()
        {
            LineConfiguration configuration = new LineConfiguration()
            {
                ChannelAccessToken = nameof(Create_InstanceIsInitialized)
            };

            HttpClient client = HttpClientFactory.Create(configuration);

            Assert.AreEqual(new Uri("https://api.line.me/v2/bot/"), client.BaseAddress);
            string authorization = client.DefaultRequestHeaders.GetValues("Authorization").First();
            Assert.AreEqual("Bearer " + nameof(Create_InstanceIsInitialized), authorization);
        }
    }
}
