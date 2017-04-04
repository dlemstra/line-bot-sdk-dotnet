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

using System.Threading.Tasks;

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for the bot that can be used to communicatie with the Line API.
    /// </summary>
    public interface ILineBot
    {
        /// <summary>
        /// Returns the profile for the specified user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>The profile for the specified user.</returns>
        Task<IUserProfile> GetProfile(string userId);

        /// <summary>
        /// Leaves the specified group.
        /// </summary>
        /// <param name="groupId">The id of the group.</param>
        /// <returns>.</returns>
        Task LeaveGroup(string groupId);
    }
}
