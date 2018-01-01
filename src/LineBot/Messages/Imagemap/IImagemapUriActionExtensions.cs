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
    internal static class IImagemapUriActionExtensions
    {
        public static ImagemapUriAction ToImagemapUriAction(this IImagemapUriAction self)
        {
            if (self.Url == null)
                throw new InvalidOperationException("The url cannot be null.");

            ImagemapUriAction imagemapUriAction = self as ImagemapUriAction;

            if (imagemapUriAction == null)
            {
                imagemapUriAction = new ImagemapUriAction()
                {
                    Url = self.Url
                };
            }

            imagemapUriAction.Area = self.Area.ToImagemapArea();

            return imagemapUriAction;
        }
    }
}
