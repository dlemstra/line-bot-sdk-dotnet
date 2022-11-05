// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
  internal sealed class GroupSummary : IGroupSummary
  {
    [JsonProperty("groupId")]
    public string GroupId { get; set; } = default!;

    [JsonProperty("groupName")]
    public string GroupName { get; set; } = default!;

    [JsonProperty("pictureUrl")]
    public Uri PictureUrl { get; set; } = default!;
  }
}