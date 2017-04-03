// <copyright file="HttpResponseMessageExtensions.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System.Net.Http;

namespace Line
{
    internal static class HttpResponseMessageExtensions
    {
        public static void CheckResult(this HttpResponseMessage self)
        {
            self.EnsureSuccessStatusCode();
        }
    }
}
