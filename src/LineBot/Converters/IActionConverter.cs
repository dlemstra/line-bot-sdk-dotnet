// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

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

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            var action = CreateAction(obj);

            serializer.Populate(obj.CreateReader(), action);

            return action;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private IAction CreateAction(JObject obj)
        {
            var type = obj.GetValue("type");
            if (type is null)
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
