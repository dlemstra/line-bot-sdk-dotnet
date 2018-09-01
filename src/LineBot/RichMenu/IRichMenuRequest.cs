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

namespace Line
{
    /// <summary>
    /// Encapsulates a rich menu request.
    /// </summary>
    public interface IRichMenuRequest
    {
        /// <summary>
        /// Gets the object which contains the width and height of the rich menu displayed in the chat.
        /// Rich menu images must be one of the following sizes: 2500x1686px or 2500x843px.
        /// </summary>
        RichMenuSize Size { get; }

        /// <summary>
        /// Gets a value indicating whether the rich menu should be displayed by default.
        /// </summary>
        bool Selected { get; }

        /// <summary>
        /// Gets the name of the rich menu. This value can be used to help manage your rich menus and is not displayed to users.
        /// Max: 300 characters.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the text displayed in the chat bar.
        /// Max: 14 characters.
        /// </summary>
        string ChatBarText { get; }

        /// <summary>
        /// Gets the array of area objects which define the coordinates and size of tappable areas
        /// Max: 20 area objects.
        /// </summary>
        RichMenuArea[] Areas { get; }
    }
}
