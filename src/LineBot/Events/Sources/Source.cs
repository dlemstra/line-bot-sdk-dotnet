// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    internal sealed class Source : IGroup, IRoom, IUser
    {
        public Source(string id)
            => Id = id;

        public string Id { get; }
    }
}
