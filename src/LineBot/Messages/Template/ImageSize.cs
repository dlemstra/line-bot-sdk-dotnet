// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

namespace Line
{
    /// <summary>
    /// Encapsulates the image sizes.
    /// </summary>
    public enum ImageSize
    {
        /// <summary>
        ///  The image fills the entire image area. Parts of the image that do not fit in the area are not displayed.
        /// </summary>
        Cover,

        /// <summary>
        /// The entire image is displayed in the image area. A background is displayed in the unused areas to the left and right of vertical images and in the areas above and below horizontal images.
        /// </summary>
        Contain,
    }
}
