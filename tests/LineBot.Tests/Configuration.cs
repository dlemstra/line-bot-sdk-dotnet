// <copyright file="Configuration.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace Line.Tests
{
    public static class Configuration
    {
        public static ILineConfiguration ForTest => new LineConfiguration
        {
            ChannelAccessToken = "Test"
        };
    }
}
