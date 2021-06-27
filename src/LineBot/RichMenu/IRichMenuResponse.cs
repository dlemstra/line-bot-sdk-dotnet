// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for rich menu response.
    /// </summary>
    public interface IRichMenuResponse
    {
        /// <summary>
        /// Gets the rich menu response id.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets or sets the array of area objects which define the coordinates and size of tappable areas.
        /// Max: 20 area objects.
        /// </summary>
        RichMenuArea[]? Areas { get; set; }

        /// <summary>
        /// Gets or sets the text displayed in the chat bar.
        /// Max: 14 characters.
        /// </summary>
        string? ChatBarText { get; set; }

        /// <summary>
        /// Gets or sets the name of the rich menu. This value can be used to help manage your rich menus and is not displayed to users.
        /// Max: 300 characters.
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the rich menu should be displayed by default.
        /// </summary>
        bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the object which contains the width and height of the rich menu displayed in the chat.
        /// Rich menu images must be one of the following sizes: 2500x1686px or 2500x843px.
        /// </summary>
        RichMenuSize? Size { get; set; }
    }
}
