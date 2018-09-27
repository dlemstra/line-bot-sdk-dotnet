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

namespace Line
{
    /// <summary>
    /// Encapsulates an imagemap message.
    /// </summary>
    public interface IImagemapMessage : IOldSendMessage
    {
        /// <summary>
        /// Gets the base url of the image.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG or PNG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max dimensions: 1024 x (max 1024).</para>
        /// <para>Max size: 1 MB.</para>
        /// </summary>
        Uri BaseUrl { get; }

        /// <summary>
        /// Gets the alternative text for devices that do not support this type of message.
        /// <para>Max: 400 characters.</para>
        /// </summary>
        string AlternativeText { get; }

        /// <summary>
        /// Gets the size of the base image (Width must be 1040).
        /// </summary>
        IImagemapSize BaseSize { get; }

        /// <summary>
        /// Gets the actions.
        /// </summary>
        IEnumerable<IImagemapAction> Actions { get; }
    }
}
