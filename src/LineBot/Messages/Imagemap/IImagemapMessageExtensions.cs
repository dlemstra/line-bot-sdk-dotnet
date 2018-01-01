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
using System.Linq;

namespace Line
{
    internal static class IImagemapMessageExtensions
    {
        public static ImagemapMessage ToImagemapMessage(this IImagemapMessage self)
        {
            if (self.BaseUrl == null)
                throw new InvalidOperationException("The base url cannot be null.");

            if (self.AlternativeText == null)
                throw new InvalidOperationException("The alternative text cannot be null.");

            if (self.BaseSize == null)
                throw new InvalidOperationException("The base size cannot be null.");

            if (self.Actions == null)
                throw new InvalidOperationException("The actions cannot be null.");

            ImagemapMessage imagemapMessage = self as ImagemapMessage;
            if (imagemapMessage == null)
            {
                imagemapMessage = new ImagemapMessage()
                {
                    BaseUrl = self.BaseUrl,
                    AlternativeText = self.AlternativeText,
                };
            }

            imagemapMessage.BaseSize = self.BaseSize.ToImagemapSize();
            imagemapMessage.Actions = self.Actions.ToImagemapActions().ToArray();

            return imagemapMessage;
        }
    }
}
