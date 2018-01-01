// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
using System.Net;

namespace Line
{
    /// <summary>
    /// Reports errors that occur when communicating with the Line API.
    /// </summary>
    public sealed class LineBotException : Exception
    {
        internal LineBotException(string message)
            : base(message)
        {
            StatusCode = HttpStatusCode.OK;
            Details = new List<ILineErrorDetails>();
        }

        internal LineBotException(HttpStatusCode statusCode)
            : base("Unknown error")
        {
            StatusCode = statusCode;
            Details = new List<ILineErrorDetails>();
        }

        internal LineBotException(HttpStatusCode statusCode, LineError error)
            : base(error.Message)
        {
            StatusCode = statusCode;
            Details = error.Details;
        }

        /// <summary>
        /// Gets the status code that was reported by the Line API.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the details of the error that occurred.
        /// </summary>
        public IEnumerable<ILineErrorDetails> Details { get; }
    }
}
