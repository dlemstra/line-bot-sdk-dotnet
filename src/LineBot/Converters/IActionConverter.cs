// Copyright 2017-2019 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet)
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
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Line
{
    internal sealed class IActionConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(IAction))
                return true;

            return objectType.GetTypeInfo().ImplementedInterfaces.Any(type => type == typeof(IAction));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            var action = CreateAction(obj);

            serializer.Populate(obj.CreateReader(), action);

            return action;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private IAction CreateAction(JObject obj)
        {
            var type = obj.GetValue("type");
            if (type == null)
                throw new InvalidOperationException("The type property is missing.");

            var typeName = type.Value<string>();

            switch (typeName)
            {
                case "camera":
                    return new CameraAction();
                case "cameraRoll":
                    return new CameraRollAction();
                case "datetimepicker":
                    return new DateTimePickerAction();
                case "location":
                    return new LocationAction();
                case "message":
                    return new MessageAction();
                case "postback":
                    return new PostbackAction();
                case "uri":
                    return new UriAction();
                default:
                    throw new NotSupportedException($"Unknown type '{typeName}'.");
            }
        }
    }
}
