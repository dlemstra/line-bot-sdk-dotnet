// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;

namespace Line
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> self, int size)
        {
            while (self.Any())
            {
                yield return self.Take(size);
                self = self.Skip(size);
            }
        }
    }
}
