// Copyright 2017-2018 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
//
// Dirk Lemstra licenses this file to you under the Apache License,
// version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at:
//
//   https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.

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

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
                throw new InvalidOperationException("Only string is supported.");

            if (!Enum.TryParse((string)reader.Value, true, out TEnum result))
            {
                result = default;
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Enum val = (Enum)value;

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