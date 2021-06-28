// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Rich menu id response object. This object is returned when you get a rich menu or get a list of rich menus.
    /// </summary>
    internal sealed class RichMenuIdResponse
    {
        /// <summary>
        /// Gets or sets the rich menu ID.
        /// </summary>
        [JsonProperty("richMenuId")]
        public string? RichMenuId { get; set; }
    }
}
