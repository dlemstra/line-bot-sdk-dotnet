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

using System;

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for a LINE event.
    /// </summary>
    public interface ILineEvent
    {
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        LineEventType EventType { get; }

        /// <summary>
        /// Gets the follow event.
        /// </summary>
        IFollowEvent FollowEvent { get; }

        /// <summary>
        /// Gets the source of the event.
        /// </summary>
        IEventSource Source { get; }

        /// <summary>
        /// Gets the time of the event.
        /// </summary>
        DateTime Timestamp { get; }
    }
}