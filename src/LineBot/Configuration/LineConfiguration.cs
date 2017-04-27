﻿// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

namespace Line
{
    /// <summary>
    /// Encapsulates the configuration of the <see cref="ILineBot"/>.
    /// </summary>
    public sealed class LineConfiguration : ILineConfiguration
    {
        /// <summary>
        /// Gets or sets the channel access token.
        /// </summary>
        public string ChannelAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the channel secret.
        /// </summary>
        public string ChannelSecret { get; set; }

        /// <summary>
        /// Creates a line bot.
        /// </summary>
        /// <returns>A line bot</returns>
        public ILineBot CreateBot()
        {
            if (string.IsNullOrWhiteSpace(ChannelAccessToken))
                throw new InvalidOperationException("ChannelAccessToken cannot be null or whitespace.");

            if (string.IsNullOrWhiteSpace(ChannelSecret))
                throw new InvalidOperationException("ChannelSecret cannot be null or whitespace.");

            return new LineBot(this, HttpClientFactory.Create(this));
        }
    }
}