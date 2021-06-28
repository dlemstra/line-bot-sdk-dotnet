// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace Line
{
    internal static class RichMenuAreaExtensions
    {
        public static void Validate(this IEnumerable<RichMenuArea> self)
        {
            foreach (var richMenuArea in self)
            {
                if (richMenuArea is null)
                    throw new InvalidOperationException("The rich menu area should not be null.");

                richMenuArea.Validate();
            }
        }
    }
}
