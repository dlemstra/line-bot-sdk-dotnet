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
    internal static class IImageCarouselColumnExtensions
    {
        public static IEnumerable<IImageCarouselColumn> ToImageCarouselColumn(this IEnumerable<IImageCarouselColumn> self)
        {
            foreach (IImageCarouselColumn column in self)
            {
                yield return column.ToImageCarouselColumn();
            }
        }

        public static ImageCarouselColumn ToImageCarouselColumn(this IImageCarouselColumn self)
        {
            if (self.ImageUrl == null)
                throw new InvalidOperationException("The ImageUrl cannot be null.");

            ImageCarouselColumn imageCarouselColumn = self as ImageCarouselColumn;
            if (imageCarouselColumn == null)
            {
                imageCarouselColumn = new ImageCarouselColumn()
                {
                    ImageUrl = self.ImageUrl,
                };
            }

            if (self.Action == null)
                throw new InvalidOperationException("The action cannot be null.");

            imageCarouselColumn.Action = TemplateAction.Convert(self.Action);

            return imageCarouselColumn;
        }
    }
}
