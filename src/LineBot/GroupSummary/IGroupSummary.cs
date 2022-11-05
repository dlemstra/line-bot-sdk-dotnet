// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;

namespace Line
{
    /// <summary>
    /// Encapsulates the interface for the summary of a group.
    /// </summary>
    public interface IGroupSummary
  {
    /// <summary>
    /// Gets the Group ID.
    /// </summary>
    string GroupId { get; }

    /// <summary>
    /// Gets the Group name.
    /// </summary>
    string GroupName { get; }

        /// <summary>
        /// Gets the Group icon URL.
        /// </summary>
    Uri PictureUrl { get; }
  }
}