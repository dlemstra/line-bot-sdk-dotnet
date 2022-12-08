// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Line.Tests
{
    [TestClass]
    public class GroupSummaryTests
    {
        [TestMethod]
        public async Task GetGroupSummmary_GroupIsNulll_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("group", async () =>
            {
                IGroupSummary summary = await bot.GetGroupSummmary((IGroup)null);
            });
        }

        [TestMethod]
        public async Task GetGroupSummmary_GroupIdIsNull_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("groupId", async () =>
            {
                IGroupSummary summary = await bot.GetGroupSummmary((string)null);
            });
        }

        [TestMethod]
        public async Task GetGroupSummmary_GroupIdIsEmpty_ThrowsException()
        {
            ILineBot bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("groupId", async () =>
            {
                IGroupSummary summary = await bot.GetGroupSummmary(string.Empty);
            });
        }

        [TestMethod]
        public async Task GetGroupSummmary_ErrorResponse_ThrowsException()
        {
            TestHttpClient httpClient = TestHttpClient.ThatReturnsAnError();

            ILineBot bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.GetGroupSummmary("test");
            });
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.GroupSummary)]
        public async Task GetGroupSummary_CorrectResponse_ReturnsGroupSummary()
        {
            TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.GroupSummary);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            IGroupSummary summary = await bot.GetGroupSummmary("test");

            Assert.AreEqual(HttpMethod.Get, httpClient.RequestMethod);
            Assert.AreEqual("/group/test/summary", httpClient.RequestPath);

            Assert.IsNotNull(summary);
            Assert.AreEqual("Ca56f94637c...", summary.GroupId);
            Assert.AreEqual("Group name", summary.GroupName);
            Assert.AreEqual(new Uri("https://profile.line-scdn.net/abcdefghijklmn"), summary.PictureUrl);
        }

        [TestMethod]
        [DeploymentItem(JsonDocuments.GroupSummary)]
        public async Task GetGroupSummary_WithGroup_ReturnsGroupSummary()
        {
            TestHttpClient httpClient = TestHttpClient.Create(JsonDocuments.GroupSummary);

            ILineBot bot = TestConfiguration.CreateBot(httpClient);
            IGroupSummary summary = await bot.GetGroupSummmary(new TestGroup());

            Assert.AreEqual("/group/testGroup/summary", httpClient.RequestPath);
            Assert.IsNotNull(summary);
        }
    }
}
