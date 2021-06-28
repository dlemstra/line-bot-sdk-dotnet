// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Line
{
    internal sealed class Guard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull(string paramName, [NotNull] object? value)
        {
            if (value is null)
                throw new ArgumentNullException(paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty(string paramName, [NotNull] string? value)
        {
            NotNull(paramName, value);

            if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.", paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty<T>(string paramName, [NotNull] T[]? value)
        {
            NotNull(paramName, value);

            if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.", paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty<T>(string paramName, [NotNull] IEnumerable<T> value)
        {
            NotNull(paramName, value);

            if (!value.Any())
                throw new ArgumentException("Value cannot be empty.", paramName);
        }
    }
}
