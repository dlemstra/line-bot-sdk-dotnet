// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates the bot that can be used to communicatie with the Line API.
    /// </summary>
    public sealed class LineBot : ILineBot
    {
        private readonly HttpClient _client;
        private readonly ILineConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LineBot"/> class.
        /// </summary>
        /// <param name="configuration">The configuration for the client.</param>
        public LineBot(ILineConfiguration configuration)
            : this(configuration, HttpClientFactory.Create(configuration))
        {
        }

        internal LineBot(ILineConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        /// <summary>
        /// Returns the profile for the specified user.
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>The profile for the specified user.</returns>
        public async Task<IUserProfile> GetProfile(string userId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);

            HttpResponseMessage response = await _client.GetAsync($"profile/{userId}");
            await response.CheckResult();

            return await response.Content.DeserializeObject<UserProfile>();
        }
    }
}
