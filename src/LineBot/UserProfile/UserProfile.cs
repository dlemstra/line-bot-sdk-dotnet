// <copyright file="UserProfile.cs" company="Dirk Lemstra">
// Copyright 2017 Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates the profile of a user.
    /// </summary>
    internal sealed class UserProfile : IUserProfile
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("pictureUrl")]
        public Uri PictureUrl { get; set; }

        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
