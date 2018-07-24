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
    /// Encapsulates a image carousel column.
    /// </summary>
    public interface IImageCarouselColumn
    {
        /// <summary>
        /// Gets the image url.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: JPEG or PNG.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Aspect ratio: 1:1.</para>
        /// <para>Max width: 1024px.</para>
        /// <para>Max size: 1 MB.</para>
        /// </summary>
        Uri ImageUrl { get; }

        /// <summary>
        /// Gets the action when image is tapped.
        /// </summary>
        ITemplateAction Action { get; }
    }
}
