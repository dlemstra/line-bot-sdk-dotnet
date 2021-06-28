// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates templates.
    /// </summary>
    public interface ITemplate
    {
        internal TemplateType Type { get; }

        /// <summary>
        /// Validates the template.
        /// </summary>
        void Validate();
    }
}
