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
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// This Converter converts interface to the type.
    /// </summary>
    /// <typeparam name="TI">The Type of Interface.</typeparam>
    /// <typeparam name="T"> The Type itself.</typeparam>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    internal sealed class ConcreteConverter<TI, T> : JsonConverter
    {
        // Source: https://stackoverflow.com/questions/47939878/how-to-deserialize-collection-of-interfaces-when-concrete-classes-contains-other
        public override bool CanConvert(Type objectType)
        {
            return typeof(TI) == objectType;
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            return serializer.Deserialize<T>(reader);
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}