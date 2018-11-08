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

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for the bot that can be used to communicatie with the Line API.
    /// </summary>
    public interface ILineBot
    {
        /// <summary>
        /// Creates a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu represented as a rich menu object.</param>
        /// <returns>.</returns>
        Task<string> CreateRichMenu(RichMenu richMenu);

        /// <summary>
        /// Cancels the default rich menu set with the Messaging API.
        /// </summary>
        /// <returns>.</returns>
        Task<ILineBot> DeleteDefaultRichMenu();

        /// <summary>
        /// Deletes a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        Task<ILineBot> DeleteRichMenu(IRichMenuResponse richMenu);

        /// <summary>
        /// Deletes a rich menu.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        Task<ILineBot> DeleteRichMenu(string richMenuId);

        /// <summary>
        /// Unlinks a rich menu from a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>.</returns>
        Task<ILineBot> DeleteUserRichMenu(IUser user);

        /// <summary>
        /// Unlinks a rich menu from a user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>.</returns>
        Task<ILineBot> DeleteUserRichMenu(string userId);

        /// <summary>
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The content of the specified message.</returns>
        Task<byte[]> GetMessageContent(IMessage message);

        /// <summary>
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="messageId">The id of the message.</param>
        /// <returns>The content of the specified message.</returns>
        Task<byte[]> GetMessageContent(string messageId);

        /// <summary>
        /// Returns the ID of the default rich menu set.
        /// </summary>
        /// <returns>The ID of the default rich menu set.</returns>
        Task<string> GetDefaultRichMenu();

        /// <summary>
        /// Returns the events from the specified request.
        /// </summary>
        /// <param name="request">The http request.</param>
        /// <returns>The events from the specified request.</returns>
        Task<IEnumerable<ILineEvent>> GetEvents(HttpRequest request);

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The profile of the specified user.</returns>
        Task<IUserProfile> GetProfile(IUser user);

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>The profile of the specified user.</returns>
        Task<IUserProfile> GetProfile(string userId);

        /// <summary>
        /// Gets a rich menu via a rich menu ID.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>A rich menu via a rich menu ID.</returns>
        Task<IRichMenuResponse> GetRichMenu(string richMenuId);

        /// <summary>
        /// Downloads an image associated with a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        Task<byte[]> GetRichMenuImage(IRichMenuResponse richMenu);

        /// <summary>
        /// Downloads an image associated with a rich menu.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        Task<byte[]> GetRichMenuImage(string richMenuId);

        /// <summary>
        /// Gets a list of all uploaded rich menus.
        /// </summary>
        /// <returns>A list of all uploaded rich menus.</returns>
        Task<IEnumerable<IRichMenuResponse>> GetRichMenus();

        /// <summary>
        /// Gets the ID of the rich menu linked to a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>.</returns>
        Task<string> GetUserRichMenu(IUser user);

        /// <summary>
        /// Gets the ID of the rich menu linked to a user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>.</returns>
        Task<string> GetUserRichMenu(string userId);

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns>.</returns>
        Task<ILineBot> LeaveGroup(IGroup group);

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="groupId">The id of the group.</param>
        /// <returns>.</returns>
        Task<ILineBot> LeaveGroup(string groupId);

        /// <summary>
        /// Leave the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>.</returns>
        Task<ILineBot> LeaveRoom(IRoom room);

        /// <summary>
        /// Leave the specified room.
        /// </summary>
        /// <param name="roomId">The id of the room.</param>
        /// <returns>.</returns>
        Task<ILineBot> LeaveRoom(string roomId);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The users that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Multicast(IEnumerable<IUser> to, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The users that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Multicast(IEnumerable<IUser> to, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// <para>Use IDs returned via the webhook event of source users. IDs of groups or rooms cannot be used. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">The IDs of the receivers.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Multicast(IEnumerable<string> to, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// <para>Use IDs returned via the webhook event of source users. IDs of groups or rooms cannot be used. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">The IDs of the receivers.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Multicast(IEnumerable<string> to, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to a group at any time.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Push(IGroup group, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to a group at any time.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Push(IGroup group, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Push(IRoom room, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Push(IRoom room, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to a user at any time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Push(IUser user, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to a user at any time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Push(IUser user, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// <para>Use the ID returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">ID of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Push(string to, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// <para>Use the ID returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</para>
        /// </summary>
        /// <param name="to">ID of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Push(string to, params ISendMessage[] messages);

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Reply(IReplyToken token, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Reply(IReplyToken token, params ISendMessage[] messages);

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Reply(string replyToken, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task<ILineBot> Reply(string replyToken, params ISendMessage[] messages);

        /// <summary>
        /// Sets the default rich menu, which is displayed to all users who have added the bot as a friend and are not linked to any per-user rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        Task<ILineBot> SetDefaultRichMenu(IRichMenuResponse richMenu);

        /// <summary>
        /// Sets the default rich menu, which is displayed to all users who have added the bot as a friend and are not linked to any per-user rich menu.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        Task<ILineBot> SetDefaultRichMenu(string richMenuId);

        /// <summary>
        /// Uploads and attaches an image to a rich menu.
        /// </summary>
        /// <param name="richMenu">The rich menu response.</param>
        /// <param name="imageData">The data of the image.</param>
        /// <returns>.</returns>
        Task<ILineBot> SetRichMenuImage(IRichMenuResponse richMenu, byte[] imageData);

        /// <summary>
        /// Uploads and attaches an image to a rich menu.
        /// </summary>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <param name="imageData">The data of the image.</param>
        /// <returns>.</returns>
        Task<ILineBot> SetRichMenuImage(string richMenuId, byte[] imageData);

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        Task<ILineBot> SetUserRichMenu(IUser user, IRichMenuResponse richMenu);

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        Task<ILineBot> SetUserRichMenu(IUser user, string richMenuId);

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="richMenu">The rich menu response.</param>
        /// <returns>.</returns>
        Task<ILineBot> SetUserRichMenu(string userId, IRichMenuResponse richMenu);

        /// <summary>
        /// Links a rich menu to a user. Only one rich menu can be linked to a user at one time.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="richMenuId">The rich menu id.</param>
        /// <returns>.</returns>
        Task<ILineBot> SetUserRichMenu(string userId, string richMenuId);
    }
}
