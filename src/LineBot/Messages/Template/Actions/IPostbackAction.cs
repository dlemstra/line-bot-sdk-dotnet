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
    /// Encapsulates a template postback action.
    /// </summary>
    public interface IPostbackAction : ITemplateAction
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <remarks>Max: 20 characters</remarks>
        string Label { get; }

        /// <summary>
        /// Gets the string returned via webhook in the postback.data property of the <see cref="IPostback"/> event.
        /// </summary>
        /// <remarks>Max: 300 characters</remarks>
        string Data { get; }

        /// <summary>
        /// Gets the text sent when the action is performed.
        /// </summary>
        /// <remarks>Max: 300 characters</remarks>
        string Text { get; }
    }
}
