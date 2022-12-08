// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class UnixDateTimeConverter : JsonConverter
    {
        public override bool CanWrite
            => false;

        public override bool CanConvert(Type objectType)
        => objectType == typeof(DateTime);

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
                throw new InvalidOperationException("Only integer is supported.");

            if (reader.Value is null)
                return default(DateTimeOffset);

            var milliseconds = (long)reader.Value;

            var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);

            return dateTimeOffset.UtcDateTime;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            => throw new NotSupportedException();
    }
}
