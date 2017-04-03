// <copyright file="Guard.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;

namespace Line
{
    internal sealed class Guard
    {
        public static void NotNull(string paramName, object value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException(paramName);
        }

        public static void NotNullOrEmpty(string paramName, string value)
        {
            NotNull(paramName, value);

            if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.", paramName);
        }
    }
}
