// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line.Tests
{
    public static class JsonSerializer
    {
        public static string SerializeObject(object value)
        {
            string serialized = JsonConvert.SerializeObject(
                value,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

            return serialized;
        }
    }
}
