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

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The content of the specified message.</returns>
        public async Task<byte[]> GetContent(IMessage message)
        {
            Guard.NotNull(nameof(message), message);

            return await GetContent(message.Id);
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
        /// Returns the events from the specified request.
        /// </summary>
        /// <param name="request">The http request.</param>
        /// <returns>The events from the specified request.</returns>
        public async Task<IEnumerable<ILineEvent>> GetEvents(HttpRequest request)
        {
            Guard.NotNull(nameof(request), request);

            byte[] content = await request.Body.ToArrayAsync();

            if (content == null)
                return Enumerable.Empty<ILineEvent>();

            string signature = request.Headers["X-Line-Signature"];

            SignatureValidator validator = new SignatureValidator(_configuration);
            validator.Validate(content, signature);

            string jsonContent = Encoding.UTF8.GetString(content);

            LineEventCollection eventCollection = JsonConvert.DeserializeObject<LineEventCollection>(jsonContent);

            if (eventCollection == null || eventCollection.Events == null)
                return Enumerable.Empty<ILineEvent>();

            return eventCollection.Events;
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

        /// <summary>
        /// Leaves the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>.</returns>
        public async Task LeaveRoom(IRoom room)
        {
            Guard.NotNull(nameof(room), room);

            await LeaveRoom(room.Id);
        }

        /// <summary>
        /// Send messages to a group at any time.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task Push(IGroup group, params ISendMessage[] messages)
        {
            Guard.NotNull(nameof(group), group);

            await Push(group.Id, messages);
        }

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task Push(IRoom room, params ISendMessage[] messages)
        {
            Guard.NotNull(nameof(room), room);

            await Push(room.Id, messages);
        }

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// </summary>
        /// <remarks>Use the id returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</remarks>
        /// <param name="to ">id of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task Push(string to, params ISendMessage[] messages)
        {
            Guard.NotNullOrEmpty(nameof(to), to);
            Guard.NotNullOrEmpty(nameof(messages), messages);

            PushMessage push = new PushMessage(to, messages);

            StringContent content = CreateStringContent(push);

            HttpResponseMessage response = await _client.PostAsync($"message/push", content);
            await response.CheckResult();
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task Reply(IReplyToken token, params ISendMessage[] messages)
        {
            Guard.NotNull(nameof(token), token);

            await Reply(token.ReplyToken, messages);
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task Reply(string replyToken, params ISendMessage[] messages)
        {
            Guard.NotNullOrEmpty(nameof(replyToken), replyToken);
            Guard.NotNullOrEmpty(nameof(messages), messages);

            ReplyMessage reply = new ReplyMessage(replyToken, messages);

            StringContent content = CreateStringContent(reply);

            HttpResponseMessage response = await _client.PostAsync($"message/reply", content);
            await response.CheckResult();
        }

        private static StringContent CreateStringContent<T>(T value)
        {
            string content = JsonConvert.SerializeObject(value);

            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
