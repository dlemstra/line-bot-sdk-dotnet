// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates actions.
    /// </summary>
    public interface IAction
    {
        internal ActionType Type { get; }

        /// <summary>
        /// Validates the action.
        /// </summary>
        void Validate();
    }
}
