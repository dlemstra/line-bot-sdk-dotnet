// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace Line
{
    internal static class IActionExtensions
    {
        public static void CheckActionType(this IAction? self)
        {
            if (self is null)
                throw new NotSupportedException("The action cannot be null.");

            if (self.Type == ActionType.Camera)
                return;

            if (self.Type == ActionType.CameraRoll)
                return;

            if (self.Type == ActionType.DateTimePicker)
                return;

            if (self.Type == ActionType.Location)
                return;

            if (self.Type == ActionType.Message)
                return;

            if (self.Type == ActionType.Postback)
                return;

            if (self.Type == ActionType.Uri)
                return;

            throw new NotSupportedException("The action type is invalid.");
        }

        public static void Validate(this IEnumerable<IAction> self)
        {
            foreach (var action in self)
            {
                action.Validate();
            }
        }
    }
}
