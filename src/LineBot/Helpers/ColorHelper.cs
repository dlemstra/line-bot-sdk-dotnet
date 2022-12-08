// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Line
{
    internal static class ColorHelper
    {
        public static void Validate([NotNull] string? value)
        {
            if (value is null || value.Length != 7)
                throw new InvalidOperationException("The color should be 7 characters long.");

            if (value[0] != '#')
                throw new InvalidOperationException("The color should start with #.");

            if (!IsValidColor(value))
                throw new InvalidOperationException("The color contains invalid characters.");
        }

        private static bool IsValidColor(string value)
        {
            for (var i = 1; i < value.Length; i++)
            {
                var character = value[i];

                // 0-9
                if (character >= 48 && character <= 57)
                    continue;

                // A-F
                if (character >= 65 && character <= 70)
                    continue;

                // a-f
                if (character >= 97 && character <= 102)
                    continue;

                return false;
            }

            return true;
        }
    }
}
