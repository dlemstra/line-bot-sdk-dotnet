// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line.Tests
{
    public class TestAction : IAction
    {
        ActionType IAction.Type
            => (ActionType)42;

        public void Validate()
        {
        }
    }
}
