// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace Line
{
    internal static class ImagemapActionExtensions
    {
        public static void Validate(this IEnumerable<ImagemapAction> self)
        {
            foreach (var imagemapAction in self)
            {
                if (imagemapAction is null)
                    throw new InvalidOperationException("The imagemap action should not be null.");

                imagemapAction.Validate();
            }
        }
    }
}
