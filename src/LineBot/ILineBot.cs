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
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The content of the specified message.</returns>
        Task<byte[]> GetContent(IMessage message);

        /// <summary>
        /// Returns the content of the specified message.
        /// </summary>
        /// <param name="messageId">The id of the message</param>
        /// <returns>The content of the specified message.</returns>
        Task<byte[]> GetContent(string messageId);

        /// <summary>
        /// Returns the events from the specified request.
        /// </summary>
        /// <param name="request">The http request.</param>
        /// <returns>The events from the specified request.</returns>
        Task<IEnumerable<ILineEvent>> GetEvents(HttpRequest request);

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>The profile of the specified user.</returns>
        Task<IUserProfile> GetProfile(string userId);

        /// <summary>
        /// Returns the profile of the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The profile of the specified user.</returns>
        Task<IUserProfile> GetProfile(IUser user);

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="groupId">The id of the group.</param>
        /// <returns>.</returns>
        Task LeaveGroup(string groupId);

        /// <summary>
        /// Leave the specified group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns>.</returns>
        Task LeaveGroup(IGroup group);

        /// <summary>
        /// Leave the specified room.
        /// </summary>
        /// <param name="roomId">The id of the room.</param>
        /// <returns>.</returns>
        Task LeaveRoom(string roomId);

        /// <summary>
        /// Leave the specified room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>.</returns>
        Task LeaveRoom(IRoom room);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The groups that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Multicast(IEnumerable<IGroup> to, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The groups that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Multicast(IEnumerable<IGroup> to, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The rooms that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Multicast(IEnumerable<IRoom> to, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The rooms that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Multicast(IEnumerable<IRoom> to, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The users that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Multicast(IEnumerable<IUser> to, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The users that should receive the messages.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Multicast(IEnumerable<IUser> to, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The IDs of the receivers.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Multicast(IEnumerable<string> to, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to multiple users at any time.
        /// </summary>
        /// <param name="to">The IDs of the receivers.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Multicast(IEnumerable<string> to, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to a group at any time.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Push(IGroup group, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to a group at any time.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Push(IGroup group, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Push(IRoom room, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to a room at any time.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Push(IRoom room, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to a user at any time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Push(IUser user, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to a user at any time.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Push(IUser user, params ISendMessage[] messages);

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// </summary>
        /// <remarks>Use the ID returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</remarks>
        /// <param name="to">ID of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Push(string to, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Send messages to a user, group, or room at any time.
        /// </summary>
        /// <remarks>Use the ID returned via the webhook event of the source user, group, or room as the ID of the receiver. Do not use the LINE ID found on the LINE app.</remarks>
        /// <param name="to">ID of the receiver.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Push(string to, params ISendMessage[] messages);

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Reply(IReplyToken token, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="token">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Reply(IReplyToken token, params ISendMessage[] messages);

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Reply(string replyToken, IEnumerable<ISendMessage> messages);

        /// <summary>
        /// Respond to events from users, groups, and rooms.
        /// </summary>
        /// <param name="replyToken">The reply token.</param>
        /// <param name="messages">The messages to send.</param>
        /// <returns>.</returns>
        Task Reply(string replyToken, params ISendMessage[] messages);
    }
}
