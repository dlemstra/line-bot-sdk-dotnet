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
    /// Encapsulates a buttons template.
    /// </summary>
    public interface IButtonsTemplate : ITemplate
    {
        /// <summary>
        /// Gets the image url for the thumbnail.
        /// </summary>
        /// <remarks>
        /// Protocol: HTTPS<para/>
        /// Format: JPEG or PNG<para/>
        /// Max url length: 1000 characters<para/>
        /// Aspect ratio: 1:1.51<para/>
        /// Max width: 1024px<para/>
        /// Max size: 1 MB
        /// </remarks>
        Uri ThumbnailUrl { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <remarks>Max: 400 characters</remarks>
        string Title { get; }

        /// <summary>
        /// Gets the message text.
        /// </summary>
        /// <remarks>
        /// Max: 160 characters (no image or title)<para/>
        /// Max: 60 characters(message with an image or title)
        /// </remarks>
        string Text { get; }

        /// <summary>
        /// Gets the actions when tapped.
        /// </summary>
        /// <remarks>
        /// Max: 4
        /// </remarks>
        IEnumerable<ITemplateAction> Actions { get; }
    }
}
