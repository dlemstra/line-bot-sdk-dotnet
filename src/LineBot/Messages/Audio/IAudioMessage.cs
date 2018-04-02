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
    /// Encapsulates an audio message.
    /// </summary>
    public interface IAudioMessage : ISendMessage
    {
        /// <summary>
        /// Gets the url of the video file.
        /// <para>Protocol: HTTPS</para>
        /// <para>Format: MP4</para>
        /// <para>Max url length: 1000 characters</para>
        /// <para>Max duration: 1024 x 1024</para>
        /// <para>Max size: 10 MB</para>
        /// </summary>
        Uri Url { get; }

        /// <summary>
        /// Gets the length of audio file in milliseconds.
        /// </summary>
        int Duration { get; }
    }
}
