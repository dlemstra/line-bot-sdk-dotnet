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
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="messageId">The id of the message</param>
        /// <returns>The content of the specified message.</returns>
        public async Task<byte[]> GetContent(string messageId)
        {
            Guard.NotNullOrEmpty(nameof(messageId), messageId);

            HttpResponseMessage response = await _client.GetAsync($"message/{messageId}/content");
            await response.CheckResult();

            if (response.Content == null)
                return null;

            return await response.Content.ReadAsByteArrayAsync();
        }

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>The profile of the specified user.</returns>
        public async Task<IUserProfile> GetProfile(string userId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);

            HttpResponseMessage response = await _client.GetAsync($"profile/{userId}");
            await response.CheckResult();

            return await response.Content.DeserializeObject<UserProfile>();
        }

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>The profile of the specified user.</returns>
        public async Task<IUserProfile> GetProfile(IUser user)
        {
            Guard.NotNull(nameof(user), user);

            return await GetProfile(user.Id);
        }

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="groupId">The id of the group.</param>
        /// <returns>.</returns>
        public async Task LeaveGroup(string groupId)
        {
            Guard.NotNullOrEmpty(nameof(groupId), groupId);

            HttpResponseMessage response = await _client.PostAsync($"group/{groupId}/leave", null);
            await response.CheckResult();
        }

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns>.</returns>
        public async Task LeaveGroup(IGroup group)
        {
            Guard.NotNull(nameof(group), group);

            await LeaveGroup(group.Id);
        }

        /// <summary>
        /// Leaves the specified room.
        /// </summary>
        /// <param name="roomId">The id of the room.</param>
        /// <returns>.</returns>
        public async Task LeaveRoom(string roomId)
        {
            Guard.NotNullOrEmpty(nameof(roomId), roomId);

            HttpResponseMessage response = await _client.PostAsync($"room/{roomId}/leave", null);
            await response.CheckResult();
        }
    }
}
