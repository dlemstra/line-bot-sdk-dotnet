// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class MulticastTests
    {
        [Fact]
        public async Task Multicast_ToIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<string>)null, new TextMessage("Test"));
            });
        }

        [Fact]
        public async Task Multicast_ToIsEmpty_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("to", async () =>
            {
                await bot.Multicast(Enumerable.Empty<string>(), new TextMessage("Test"));
            });
        }

        [Fact]
        public async Task Multicast_MessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Multicast(new string[] { "id" }, null);
            });
        }

        [Fact]
        public async Task Multicast_NoMessages_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentEmptyExceptionAsync("messages", async () =>
            {
                await bot.Multicast(new string[] { "id" }, new TextMessage[] { });
            });
        }

        [Fact]
        public async Task Multicast_EnumerableMessagesIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Multicast(new string[] { "id" }, (IEnumerable<ISendMessage>)null);
            });
        }

        [Fact]
        public async Task Multicast_ErrorResponse_ThrowsException()
        {
            var httpClient = TestHttpClient.ThatReturnsAnError();

            var bot = TestConfiguration.CreateBot(httpClient);

            await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await bot.Multicast(new string[] { "id" }, new TextMessage("Test"));
            });
        }

        [Fact]
        public async Task Multicast_CorrectInput_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Multicast(new string[] { "id1", "id2" }, new TextMessage("Test"));

            var postedData = @"{""to"":[""id1"",""id2""],""messages"":[{""type"":""text"",""text"":""Test""}]}";

            Assert.Equal("/message/multicast", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Multicast_WithEnumerable_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Multicast(new string[] { "id" }, messages);

            var postedData = @"{""to"":[""id""],""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/multicast", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Multicast_With6Messages_CallsApi2Times()
        {
            var httpClient = TestHttpClient.Create();

            var messages = new TextMessage[6];
            for (var i = 0; i < messages.Length; i++)
                messages[i] = new TextMessage("Test" + i);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Multicast(new string[] { "id1", "id2" }, messages);

            var requests = httpClient.Requests.ToArray();
            Assert.Equal(2, requests.Length);

            var postedData = @"{""to"":[""id1"",""id2""],""messages"":[{""type"":""text"",""text"":""Test0""},{""type"":""text"",""text"":""Test1""},{""type"":""text"",""text"":""Test2""},{""type"":""text"",""text"":""Test3""},{""type"":""text"",""text"":""Test4""}]}";

            Assert.Equal("/message/multicast", requests[0].RequestUri.PathAndQuery);
            Assert.Equal(postedData, requests[0].GetPostedData());

            postedData = @"{""to"":[""id1"",""id2""],""messages"":[{""type"":""text"",""text"":""Test5""}]}";

            Assert.Equal("/message/multicast", requests[1].RequestUri.PathAndQuery);
            Assert.Equal(postedData, requests[1].GetPostedData());
        }

        [Fact]
        public async Task Multicast_With201Ids_CallsApi2Times()
        {
            var httpClient = TestHttpClient.Create();

            var ids = new string[151];
            for (var i = 0; i < ids.Length; i++)
                ids[i] = "id" + i;

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Multicast(ids, new TextMessage("Test"));

            var postedData = @"{""to"":[""id0"",""id1"",""id2"",""id3"",""id4"",""id5"",""id6"",""id7"",""id8"",""id9"",""id10"",""id11"",""id12"",""id13"",""id14"",""id15"",""id16"",""id17"",""id18"",""id19"",""id20"",""id21"",""id22"",""id23"",""id24"",""id25"",""id26"",""id27"",""id28"",""id29"",""id30"",""id31"",""id32"",""id33"",""id34"",""id35"",""id36"",""id37"",""id38"",""id39"",""id40"",""id41"",""id42"",""id43"",""id44"",""id45"",""id46"",""id47"",""id48"",""id49"",""id50"",""id51"",""id52"",""id53"",""id54"",""id55"",""id56"",""id57"",""id58"",""id59"",""id60"",""id61"",""id62"",""id63"",""id64"",""id65"",""id66"",""id67"",""id68"",""id69"",""id70"",""id71"",""id72"",""id73"",""id74"",""id75"",""id76"",""id77"",""id78"",""id79"",""id80"",""id81"",""id82"",""id83"",""id84"",""id85"",""id86"",""id87"",""id88"",""id89"",""id90"",""id91"",""id92"",""id93"",""id94"",""id95"",""id96"",""id97"",""id98"",""id99"",""id100"",""id101"",""id102"",""id103"",""id104"",""id105"",""id106"",""id107"",""id108"",""id109"",""id110"",""id111"",""id112"",""id113"",""id114"",""id115"",""id116"",""id117"",""id118"",""id119"",""id120"",""id121"",""id122"",""id123"",""id124"",""id125"",""id126"",""id127"",""id128"",""id129"",""id130"",""id131"",""id132"",""id133"",""id134"",""id135"",""id136"",""id137"",""id138"",""id139"",""id140"",""id141"",""id142"",""id143"",""id144"",""id145"",""id146"",""id147"",""id148"",""id149""],""messages"":[{""type"":""text"",""text"":""Test""}]}";

            var requests = httpClient.Requests.ToArray();
            Assert.Equal(2, requests.Length);

            Assert.Equal("/message/multicast", requests[0].RequestUri.PathAndQuery);
            Assert.Equal(postedData, requests[0].GetPostedData());

            postedData = @"{""to"":[""id150""],""messages"":[{""type"":""text"",""text"":""Test""}]}";

            Assert.Equal("/message/multicast", requests[1].RequestUri.PathAndQuery);
            Assert.Equal(postedData, requests[1].GetPostedData());
        }

        [Fact]
        public async Task Multicast_UserIsNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<IUser>)null, new TextMessage("Test"));
            });
        }

        [Fact]
        public async Task Multicast_UserIsNullWithEnumerable_ThrowsException()
        {
            var messages = Enumerable.Repeat(new TextMessage(), 2);

            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("to", async () =>
            {
                await bot.Multicast((IEnumerable<IUser>)null, messages);
            });
        }

        [Fact]
        public async Task Multicast_WithUserAndMessagesAreNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Multicast(new TestUser[] { new TestUser() }, null);
            });
        }

        [Fact]
        public async Task Multicast_WithUserAndEnumerableMessagesAreNull_ThrowsException()
        {
            var bot = TestConfiguration.CreateBot();
            await ExceptionAssert.ThrowsArgumentNullExceptionAsync("messages", async () =>
            {
                await bot.Multicast(new TestUser[] { new TestUser() }, (IEnumerable<ISendMessage>)null);
            });
        }

        [Fact]
        public async Task Multicast_WithUser_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Multicast(new TestUser[] { new TestUser() }, new TextMessage("FooBar"));

            var postedData = @"{""to"":[""testUser""],""messages"":[{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/multicast", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }

        [Fact]
        public async Task Multicast_WithUserAndEnumerable_CallsApi()
        {
            var httpClient = TestHttpClient.Create();

            var messages = Enumerable.Repeat(new TextMessage("FooBar"), 2);

            var bot = TestConfiguration.CreateBot(httpClient);
            await bot.Multicast(new TestUser[] { new TestUser() }, messages);

            var postedData = @"{""to"":[""testUser""],""messages"":[{""type"":""text"",""text"":""FooBar""},{""type"":""text"",""text"":""FooBar""}]}";

            Assert.Equal("/message/multicast", httpClient.RequestPath);
            Assert.Equal(postedData, httpClient.PostedData);
        }
    }
}
