// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Line.Tests
{
    public class HttpResponseMessageExtensionsTests
    {
        [Fact]
        public async Task CheckResult_IsSuccess_ThrowsNoException()
        {
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };

            await responseMessage.CheckResult();
        }

        [Fact]
        public async Task CheckResult_ResponseIsNull_ThrowsException()
        {
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
            };

            var exception = await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await responseMessage.CheckResult();
            });

            Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
            Assert.NotNull(exception.Details);
            Assert.Empty(exception.Details);
        }

        [Fact]
        public async Task CheckResult_ResponseIsEmpyString_ThrowsException()
        {
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(string.Empty)
            };

            var exception = await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await responseMessage.CheckResult();
            });
        }

        [Fact]
        public async Task CheckResult_ResponseIsEmpyErrorObject_ThrowsException()
        {
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("{}")
            };

            var exception = await ExceptionAssert.ThrowsUnknownError(async () =>
            {
                await responseMessage.CheckResult();
            });
        }

        [Fact]
        public async Task CheckResult_ResponseIsError_ThrowsException()
        {
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(File.ReadAllText(JsonDocuments.Error))
            };

            var exception = await ExceptionAssert.ThrowsAsync<LineBotException>("The request body has 2 error(s)", async () =>
            {
                await responseMessage.CheckResult();
            });

            Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
            Assert.NotNull(exception.Details);

            var details = exception.Details.ToList();
            Assert.Equal("May not be empty", details[0].Message);
            Assert.Equal("messages[0].text", details[0].Property);
            Assert.Equal("Must be one of the following values: [text, image, video, audio, location, sticker, template, imagemap]", details[1].Message);
            Assert.Equal("messages[1].type", details[1].Property);
        }
    }
}
