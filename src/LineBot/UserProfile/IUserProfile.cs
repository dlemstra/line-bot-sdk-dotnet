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

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for the profile of a user.
    /// </summary>
    public interface IUserProfile
    {
        /// <summary>
        /// Gets the display name of the user.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the url of the picture of the user.
        /// </summary>
        Uri PictureUrl { get; }

        /// <summary>
        /// Gets the status message of the user.
        /// </summary>
        string StatusMessage { get; }

        /// <summary>
        /// Gets the id of the user.
        /// </summary>
        string UserId { get; }
    }
}