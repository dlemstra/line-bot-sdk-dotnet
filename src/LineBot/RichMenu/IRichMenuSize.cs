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
    /// Encapsulates a rich menu size.
    /// </summary>
    public interface IRichMenuSize
    {
        /// <summary>
        /// Gets the width of the rich menu. Must be 2500.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets or sets the height of the rich menu. Possible values: 1686, 843.
        /// </summary>
        int Height { get; set; }
    }
}