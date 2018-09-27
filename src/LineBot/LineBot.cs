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
        private readonly ILineBotLogger _logger;
        private readonly HttpClient _client;
        private readonly SignatureValidator _signatureValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LineBot"/> class.
        /// </summary>
        /// <param name="configuration">The configuration for the client.</param>
        public LineBot(ILineConfiguration configuration)
            : this(configuration, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineBot"/> class.
        /// </summary>
        /// <param name="configuration">The configuration for the client.</param>
        /// <param name="logger">The logger.</param>
        public LineBot(ILineConfiguration configuration, ILineBotLogger logger)
            : this(configuration, null, logger)
        {
        }

        internal LineBot(ILineConfiguration configuration, HttpClient client, ILineBotLogger logger)
        {
            Guard.NotNull(nameof(configuration), configuration);

            if (string.IsNullOrWhiteSpace(configuration.ChannelAccessToken))
                throw new ArgumentException($"The {nameof(configuration.ChannelAccessToken)} cannot be null or whitespace.", nameof(configuration));

            if (string.IsNullOrWhiteSpace(configuration.ChannelSecret))
                throw new ArgumentException($"The {nameof(configuration.ChannelSecret)} cannot be null or whitespace.", nameof(configuration));

            _logger = logger ?? new EmptyLineBotLogger();
            _client = client ?? HttpClientFactory.Create(configuration, _logger);
            _signatureValidator = new SignatureValidator(configuration);
        }

        /// <summary>
        /// Creates a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu represented as a rich menu object.</param>
        /// <returns>.</returns>
        public async Task<string> CreateRichMenu(IRichMenu richMenu)
        {
            Guard.NotNull(nameof(richMenu), richMenu);

            StringContent content = CreateStringContent(RichMenu.Convert(richMenu));

            HttpResponseMessage response = await _client.PostAsync($"richmenu", content);
            await response.CheckResult();

            string stringResponseResult = await response.Content.ReadAsStringAsync();

            var objectResult = JsonConvert.DeserializeObject<RichMenuIdResponse>(stringResponseResult);

            return objectResult.RichMenuId;
        }

        /// <summary>
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The content of the specified message.</returns>
        public async Task<byte[]> GetMessageContent(IMessage message)
        {
            Guard.NotNull(nameof(message), message);

            return await GetMessageContent(message.Id);
        }

        /// <summary>
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="messageId">The id of the message.</param>
        /// <returns>The content of the specified message.</returns>
        public async Task<byte[]> GetMessageContent(string messageId)
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

            await _logger.LogReceivedEvents(content);

            string signature = request.Headers["X-Line-Signature"];

            _signatureValidator.Validate(content, signature);

            string jsonContent = Encoding.UTF8.GetString(content);

            LineEventCollection eventCollection = JsonConvert.DeserializeObject<LineEventCollection>(jsonContent);

            if (eventCollection == null || eventCollection.Events == null)
                return Enumerable.Empty<ILineEvent>();

            return eventCollection.Events;
        }

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
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
        /// <param name="user">The user.</param>
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
        public async Task<ILineBot> LeaveGroup(string groupId)
        {
            Guard.NotNullOrEmpty(nameof(groupId), groupId);

            HttpResponseMessage response = await _client.PostAsync($"group/{groupId}/leave", null);
            await response.CheckResult();

            return this;
        }

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> LeaveGroup(IGroup group)
        {
            Guard.NotNull(nameof(group), group);

            await LeaveGroup(group.Id);

            return this;
        }

        /// <summary>
        /// Leave the specified room.
        /// </summary>
        /// <param name="roomId">The id of the room.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> LeaveRoom(string roomId)
        {
            Guard.NotNullOrEmpty(nameof(roomId), roomId);

            HttpResponseMessage response = await _client.PostAsync($"room/{roomId}/leave", null);
            await response.CheckResult();

            return this;
        }

        /// <summary>
        /// Leave the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> LeaveRoom(IRoom room)
        {
            Guard.NotNull(nameof(room), room);

            await LeaveRoom(room.Id);

            return this;
        }

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The users that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Multicast(IEnumerable<IUser> to, IEnumerable<IOldSendMessage> messages)
        {
            await Multicast(to, messages?.ToArray());

            return this;
        }

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The users that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Multicast(IEnumerable<IUser> to, params IOldSendMessage[] messages)
        {
            Guard.NotNull(nameof(to), to);

            await Multicast(to.Select(g => g.Id), messages);

            return this;
        }

        /// <summary>
        /// Send messages to multiple users at any time.
        /// <para>Use IDs returned via the webhook event of source users. IDs of groups or rooms cannot be used. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">The IDs of the receivers.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Multicast(IEnumerable<string> to, IEnumerable<IOldSendMessage> messages)
        {
            await Multicast(to, messages?.ToArray());

            return this;
        }

        /// <summary>
        /// Send messages to multiple users at any time.
        /// <para>Use IDs returned via the webhook event of source users. IDs of groups or rooms cannot be used. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">The IDs of the receivers.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Multicast(IEnumerable<string> to, params IOldSendMessage[] messages)
        {
            Guard.NotNullOrEmpty(nameof(to), to);
            Guard.NotNullOrEmpty(nameof(messages), messages);

            foreach (IEnumerable<string> toSet in to.Split(150))
            {
                foreach (IEnumerable<IOldSendMessage> messageSet in messages.Split(5))
                {
                    MulticastMessage multicast = new MulticastMessage(toSet, messageSet);

                    StringContent content = CreateStringContent(multicast);

                    HttpResponseMessage response = await _client.PostAsync($"message/multicast", content);
                    await response.CheckResult();
                }
            }

            return this;
        }

        /// <summary>
        /// Send messages to a group at any time.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(IGroup group, IEnumerable<IOldSendMessage> messages)
        {
            await Push(group, messages?.ToArray());

            return this;
        }

        /// <summary>
        /// Send messages to a group at any time.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(IGroup group, params IOldSendMessage[] messages)
        {
            Guard.NotNull(nameof(group), group);

            await Push(group.Id, messages);

            return this;
        }

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(IRoom room, IEnumerable<IOldSendMessage> messages)
        {
            await Push(room, messages?.ToArray());

            return this;
        }

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(IRoom room, params IOldSendMessage[] messages)
        {
            Guard.NotNull(nameof(room), room);

            await Push(room.Id, messages);

            return this;
        }

        /// <summary>
        /// Send messages to a user at any time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(IUser user, IEnumerable<IOldSendMessage> messages)
        {
            await Push(user, messages?.ToArray());

            return this;
        }

        /// <summary>
        /// Send messages to a user at any time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(IUser user, params IOldSendMessage[] messages)
        {
            Guard.NotNull(nameof(user), user);

            await Push(user.Id, messages);

            return this;
        }

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// <para>Use the ID returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">ID of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(string to, IEnumerable<IOldSendMessage> messages)
        {
            await Push(to, messages?.ToArray());

            return this;
        }

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// <para>Use the ID returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">ID of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(string to, params IOldSendMessage[] messages)
        {
            Guard.NotNullOrEmpty(nameof(to), to);
            Guard.NotNullOrEmpty(nameof(messages), messages);

            foreach (IEnumerable<IOldSendMessage> messageSet in messages.Split(5))
            {
                PushMessage push = new PushMessage(to, messageSet);

                StringContent content = CreateStringContent(push);

                HttpResponseMessage response = await _client.PostAsync($"message/push", content);
                await response.CheckResult();
            }

            return this;
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Reply(IReplyToken token, IEnumerable<IOldSendMessage> messages)
        {
            await Reply(token, messages?.ToArray());

            return this;
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Reply(IReplyToken token, params IOldSendMessage[] messages)
        {
            Guard.NotNull(nameof(token), token);

            await Reply(token.ReplyToken, messages);

            return this;
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Reply(string replyToken, IEnumerable<IOldSendMessage> messages)
        {
            await Reply(replyToken, messages?.ToArray());

            return this;
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Reply(string replyToken, params IOldSendMessage[] messages)
        {
            Guard.NotNullOrEmpty(nameof(replyToken), replyToken);
            Guard.NotNullOrEmpty(nameof(messages), messages);

            ReplyMessage reply = new ReplyMessage(replyToken, messages);

            StringContent content = CreateStringContent(reply);

            HttpResponseMessage response = await _client.PostAsync($"message/reply", content);
            await response.CheckResult();

            return this;
        }

        private static StringContent CreateStringContent<T>(T value)
        {
            string content = JsonConvert.SerializeObject(value);

            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
