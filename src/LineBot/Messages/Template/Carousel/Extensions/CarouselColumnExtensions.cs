// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace Line
{
    internal static class CarouselColumnExtensions
    {
        public static void Validate(this IEnumerable<CarouselColumn> self)
        {
            foreach (var column in self)
            {
                if (column is null)
                    throw new InvalidOperationException("The column should not be null.");

                column.Validate();
            }
        }
    }
}
