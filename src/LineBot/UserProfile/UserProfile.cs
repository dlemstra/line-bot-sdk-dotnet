// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    internal sealed class UserProfile : IUserProfile
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = default!;

        [JsonProperty("pictureUrl")]
        public Uri PictureUrl { get; set; } = default!;

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; } = default!;

        [JsonProperty("userId")]
        public string UserId { get; set; } = default!;
    }
}
