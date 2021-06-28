// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

        internal LineBot(ILineConfiguration configuration, HttpClient? client, ILineBotLogger? logger)
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
        public async Task<string?> CreateRichMenu(RichMenu richMenu)
        {
            Guard.NotNull(nameof(richMenu), richMenu);

            richMenu.Validate();

            var content = CreateStringContent(richMenu);

            var response = await _client.PostAsync($"richmenu", content).ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            var richMenuIdResponse = await response.Content.DeserializeObject<RichMenuIdResponse>().ConfigureAwait(false);

            return richMenuIdResponse?.RichMenuId;
        }

        /// <summary>
        /// Cancels the default rich menu set with the Messaging API.
        /// </summary>
        /// <returns>.</returns>
        public async Task<ILineBot> DeleteDefaultRichMenu()
        {
            var response = await _client.DeleteAsync($"user/all/richmenu").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return this;
        }

        /// <summary>
        /// Deletes a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        public Task<ILineBot> DeleteRichMenu(IRichMenuResponse richMenu)
        {
            Guard.NotNull(nameof(richMenu), richMenu);

            return DeleteRichMenu(richMenu.Id);
        }

        /// <summary>
        /// Deletes a rich menu.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> DeleteRichMenu(string richMenuId)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);

            var response = await _client.DeleteAsync($"richmenu/{richMenuId}").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return this;
        }

        /// <summary>
        /// Unlinks a rich menu from a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>.</returns>
        public Task<ILineBot> DeleteUserRichMenu(IUser user)
        {
            Guard.NotNull(nameof(user), user);

            return DeleteUserRichMenu(user.Id);
        }

        /// <summary>
        /// Unlinks a rich menu from a user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> DeleteUserRichMenu(string userId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);

            var response = await _client.DeleteAsync($"user/{userId}/richmenu").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return this;
        }

        /// <summary>
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The content of the specified message.</returns>
        public Task<byte[]?> GetMessageContent(IMessage message)
        {
            Guard.NotNull(nameof(message), message);

            return GetMessageContent(message.Id);
        }

        /// <summary>
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="messageId">The id of the message.</param>
        /// <returns>The content of the specified message.</returns>
        public async Task<byte[]?> GetMessageContent(string messageId)
        {
            Guard.NotNullOrEmpty(nameof(messageId), messageId);

            var response = await _client.GetAsync($"message/{messageId}/content").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            if (response.Content is null)
                return null;

            return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the ID of the default rich menu set.
        /// </summary>
        /// <returns>The ID of the default rich menu set.</returns>
        public async Task<string?> GetDefaultRichMenu()
        {
            var response = await _client.GetAsync($"user/all/richmenu").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            var richMenuIdResponse = await response.Content.DeserializeObject<RichMenuIdResponse>().ConfigureAwait(false);

            return richMenuIdResponse?.RichMenuId;
        }

        /// <summary>
        /// Returns the events from the specified request.
        /// </summary>
        /// <param name="request">The http request.</param>
        /// <returns>The events from the specified request.</returns>
        public async Task<ILineEvents> GetEvents(HttpRequest request)
        {
            Guard.NotNull(nameof(request), request);

            var content = await request.Body.ToArrayAsync().ConfigureAwait(false);

            if (content is null)
                return LineEventCollection.Empty();

            await _logger.LogReceivedEvents(content).ConfigureAwait(false);

            var signature = request.Headers["X-Line-Signature"];

            _signatureValidator.Validate(content, signature);

            var jsonContent = Encoding.UTF8.GetString(content);

            var eventCollection = JsonConvert.DeserializeObject<LineEventCollection>(jsonContent);

            if (eventCollection is null)
                return LineEventCollection.Empty();

            if (eventCollection.Events is null)
                eventCollection.Events = new List<LineEvent>();

            return eventCollection;
        }

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The profile of the specified user.</returns>
        public Task<IUserProfile?> GetProfile(IUser user)
        {
            Guard.NotNull(nameof(user), user);

            return GetProfile(user.Id);
        }

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>The profile of the specified user.</returns>
        public async Task<IUserProfile?> GetProfile(string userId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);

            var response = await _client.GetAsync($"profile/{userId}").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return await response.Content.DeserializeObject<UserProfile>().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a rich menu via a rich menu ID.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>A rich menu via a rich menu ID.</returns>
        public async Task<IRichMenuResponse?> GetRichMenu(string richMenuId)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);

            var response = await _client.GetAsync($"richmenu/{richMenuId}").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return await response.Content.DeserializeObject<RichMenuResponse>().ConfigureAwait(false);
        }

        /// <summary>
        /// Downloads an image associated with a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        public Task<byte[]> GetRichMenuImage(IRichMenuResponse richMenu)
        {
            Guard.NotNull(nameof(richMenu), richMenu);

            return GetRichMenuImage(richMenu.Id);
        }

        /// <summary>
        /// Downloads an image associated with a rich menu.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        public async Task<byte[]> GetRichMenuImage(string richMenuId)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);

            var response = await _client.GetAsync($"richmenu/{richMenuId}/content").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a list of all uploaded rich menus.
        /// </summary>
        /// <returns>A list of all uploaded rich menus.</returns>
        public async Task<IEnumerable<IRichMenuResponse>> GetRichMenus()
        {
            var response = await _client.GetAsync($"richmenu/list").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            var richMenuResponseCollection = await response.Content.DeserializeObject<RichMenuResponseCollection>().ConfigureAwait(false);

            if (richMenuResponseCollection is null || richMenuResponseCollection.RichMenus is null)
                return Enumerable.Empty<IRichMenuResponse>();

            return richMenuResponseCollection.RichMenus;
        }

        /// <summary>
        /// Gets the ID of the rich menu linked to a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>.</returns>
        public Task<string?> GetUserRichMenu(IUser user)
        {
            Guard.NotNull(nameof(user), user);

            return GetUserRichMenu(user.Id);
        }

        /// <summary>
        /// Gets the ID of the rich menu linked to a user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>.</returns>
        public async Task<string?> GetUserRichMenu(string userId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);

            var response = await _client.GetAsync($"user/{userId}/richmenu").ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            var richMenuIdResponse = await response.Content.DeserializeObject<RichMenuIdResponse>().ConfigureAwait(false);

            return richMenuIdResponse?.RichMenuId;
        }

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns>.</returns>
        public Task<ILineBot> LeaveGroup(IGroup group)
        {
            Guard.NotNull(nameof(group), group);

            return LeaveGroup(group.Id);
        }

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="groupId">The id of the group.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> LeaveGroup(string groupId)
        {
            Guard.NotNullOrEmpty(nameof(groupId), groupId);

            var response = await _client.PostAsync($"group/{groupId}/leave", null).ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

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

            var response = await _client.PostAsync($"room/{roomId}/leave", null).ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return this;
        }

        /// <summary>
        /// Leave the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>.</returns>
        public Task<ILineBot> LeaveRoom(IRoom room)
        {
            Guard.NotNull(nameof(room), room);

            return LeaveRoom(room.Id);
        }

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The users that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Multicast(IEnumerable<IUser> to, IEnumerable<ISendMessage> messages)
        {
            return Multicast(to, messages?.ToArray()!);
        }

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The users that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Multicast(IEnumerable<IUser> to, params ISendMessage[] messages)
        {
            Guard.NotNull(nameof(to), to);

            return Multicast(to.Select(g => g.Id), messages);
        }

        /// <summary>
        /// Send messages to multiple users at any time.
        /// <para>Use IDs returned via the webhook event of source users. IDs of groups or rooms cannot be used. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">The IDs of the receivers.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Multicast(IEnumerable<string> to, IEnumerable<ISendMessage> messages)
        {
            return Multicast(to, messages?.ToArray()!);
        }

        /// <summary>
        /// Send messages to multiple users at any time.
        /// <para>Use IDs returned via the webhook event of source users. IDs of groups or rooms cannot be used. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">The IDs of the receivers.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Multicast(IEnumerable<string> to, params ISendMessage[] messages)
        {
            Guard.NotNullOrEmpty(nameof(to), to);
            Guard.NotNullOrEmpty(nameof(messages), messages);

            foreach (IEnumerable<string> toSet in to.Split(150))
            {
                foreach (IEnumerable<ISendMessage> messageSet in messages.Split(5))
                {
                    var multicast = new MulticastMessage(toSet, messageSet);

                    var content = CreateStringContent(multicast);

                    var response = await _client.PostAsync($"message/multicast", content).ConfigureAwait(false);
                    await response.CheckResult().ConfigureAwait(false);
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
        public Task<ILineBot> Push(IGroup group, IEnumerable<ISendMessage> messages)
        {
            return Push(group, messages?.ToArray()!);
        }

        /// <summary>
        /// Send messages to a group at any time.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Push(IGroup group, params ISendMessage[] messages)
        {
            Guard.NotNull(nameof(group), group);

            return Push(group.Id, messages);
        }

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Push(IRoom room, IEnumerable<ISendMessage> messages)
        {
            return Push(room, messages?.ToArray()!);
        }

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Push(IRoom room, params ISendMessage[] messages)
        {
            Guard.NotNull(nameof(room), room);

            return Push(room.Id, messages);
        }

        /// <summary>
        /// Send messages to a user at any time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Push(IUser user, IEnumerable<ISendMessage> messages)
        {
            return Push(user, messages?.ToArray()!);
        }

        /// <summary>
        /// Send messages to a user at any time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Push(IUser user, params ISendMessage[] messages)
        {
            Guard.NotNull(nameof(user), user);

            return Push(user.Id, messages);
        }

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// <para>Use the ID returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">ID of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Push(string to, IEnumerable<ISendMessage> messages)
        {
            return Push(to, messages?.ToArray()!);
        }

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// <para>Use the ID returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">ID of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Push(string to, params ISendMessage[] messages)
        {
            Guard.NotNullOrEmpty(nameof(to), to);
            Guard.NotNullOrEmpty(nameof(messages), messages);

            foreach (IEnumerable<ISendMessage> messageSet in messages.Split(5))
            {
                var push = new PushMessage(to, messageSet);

                var content = CreateStringContent(push);

                var response = await _client.PostAsync($"message/push", content).ConfigureAwait(false);
                await response.CheckResult().ConfigureAwait(false);
            }

            return this;
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Reply(IReplyToken token, IEnumerable<ISendMessage> messages)
        {
            return Reply(token, messages?.ToArray()!);
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Reply(IReplyToken token, params ISendMessage[] messages)
        {
            Guard.NotNull(nameof(token), token);

            return Reply(token.ReplyToken!, messages);
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public Task<ILineBot> Reply(string replyToken, IEnumerable<ISendMessage> messages)
        {
            return Reply(replyToken, messages?.ToArray()!);
        }

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> Reply(string replyToken, params ISendMessage[] messages)
        {
            Guard.NotNullOrEmpty(nameof(replyToken), replyToken);
            Guard.NotNullOrEmpty(nameof(messages), messages);

            var reply = new ReplyMessage(replyToken, messages);

            var content = CreateStringContent(reply);

            var response = await _client.PostAsync($"message/reply", content).ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return this;
        }

        /// <summary>
        /// Sets the default rich menu, which is displayed to all users who have added the bot as a friend and are not linked to any per-user rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        public Task<ILineBot> SetDefaultRichMenu(IRichMenuResponse richMenu)
        {
            Guard.NotNull(nameof(richMenu), richMenu);

            return SetDefaultRichMenu(richMenu.Id);
        }

        /// <summary>
        /// Sets the default rich menu, which is displayed to all users who have added the bot as a friend and are not linked to any per-user rich menu.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> SetDefaultRichMenu(string richMenuId)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);

            var response = await _client.PostAsync($"user/all/richmenu/{richMenuId}", null).ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return this;
        }

        /// <summary>
        /// Uploads and attaches an image to a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu response.</param>
        /// <param name="imageData">The data of the image.</param>
        /// <returns>.</returns>
        public Task<ILineBot> SetRichMenuImage(IRichMenuResponse richMenu, byte[] imageData)
        {
            Guard.NotNull(nameof(richMenu), richMenu);

            return SetRichMenuImage(richMenu.Id, imageData);
        }

        /// <summary>
        /// Uploads and attaches an image to a rich menu.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <param name="imageData">The data of the image.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> SetRichMenuImage(string richMenuId, byte[] imageData)
        {
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);
            Guard.NotNullOrEmpty(nameof(imageData), imageData);

            var content = new ByteArrayContent(imageData);

            var response = await _client.PostAsync($"richmenu/{richMenuId}/content", content).ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return this;
        }

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        public Task<ILineBot> SetUserRichMenu(IUser user, IRichMenuResponse richMenu)
        {
            Guard.NotNull(nameof(user), user);
            Guard.NotNull(nameof(richMenu), richMenu);

            return SetUserRichMenu(user.Id, richMenu.Id);
        }

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        public Task<ILineBot> SetUserRichMenu(IUser user, string richMenuId)
        {
            Guard.NotNull(nameof(user), user);

            return SetUserRichMenu(user.Id, richMenuId);
        }

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        public Task<ILineBot> SetUserRichMenu(string userId, IRichMenuResponse richMenu)
        {
            Guard.NotNull(nameof(richMenu), richMenu);

            return SetUserRichMenu(userId, richMenu.Id);
        }

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        public async Task<ILineBot> SetUserRichMenu(string userId, string richMenuId)
        {
            Guard.NotNullOrEmpty(nameof(userId), userId);
            Guard.NotNullOrEmpty(nameof(richMenuId), richMenuId);

            var response = await _client.PostAsync($"user/{userId}/richmenu/{richMenuId}", null).ConfigureAwait(false);
            await response.CheckResult().ConfigureAwait(false);

            return this;
        }

        private static StringContent CreateStringContent<T>(T value)
        {
            var content = JsonConvert.SerializeObject(
                value,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}