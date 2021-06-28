// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class EnumConverter<TEnum> : JsonConverter
        where TEnum : struct, Enum
    {
        private readonly bool _lowercase;

        public EnumConverter()
            : this(true)
        {
        }

        public EnumConverter(bool lowercase)
        {
            _lowercase = lowercase;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
                throw new InvalidOperationException("Only string is supported.");

            if (reader.Value is null)
                return default(TEnum);

            if (!Enum.TryParse((string)reader.Value, true, out TEnum result))
                result = default;

            return result;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is null)
                throw new InvalidOperationException("Value cannot be null");

            var val = (Enum)value;

            var strValue = val.ToString("G");
            if (_lowercase)
            {
                strValue = strValue.ToLowerInvariant();
            }
            else
            {
                var characters = strValue.ToCharArray();
                characters[0] = char.ToLowerInvariant(characters[0]);
                strValue = new string(characters);
            }

            writer.WriteValue(strValue);
        }
    }
}