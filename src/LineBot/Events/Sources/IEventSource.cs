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
    /// Encapsulates the event source.
    /// </summary>
    public interface IEventSource
    {
        /// <summary>
        /// Gets the group.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="SourceType"/> is not <see cref="EventSourceType.Group"/>.</exception>
        /// <returns>The group.</returns>
        IGroup Group { get; }

        /// <summary>
        /// Gets the room.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="SourceType"/> is not <see cref="EventSourceType.Room"/>.</exception>
        /// <returns>The room.</returns>
        IRoom Room { get; }

        /// <summary>
        /// Gets the type of the source
        /// </summary>
        EventSourceType SourceType { get; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the <see cref="SourceType"/> is not <see cref="EventSourceType.User"/>.</exception>
        /// <returns>The user.</returns>
        IUser User { get; }
    }
}
