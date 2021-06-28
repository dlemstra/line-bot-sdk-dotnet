// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a carousel template.
    /// </summary>
    public sealed class CarouselTemplate : ITemplate
    {
        private IEnumerable<CarouselColumn>? _columns;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<TemplateType>))]
        TemplateType ITemplate.Type
            => TemplateType.Carousel;

        /// <summary>
        /// Gets or sets the columns.
        /// <para>Max: 10.</para>
        /// </summary>
        [JsonProperty("columns")]
        public IEnumerable<CarouselColumn>? Columns
        {
            get => _columns;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The columns cannot be null.");

                int count = value.Count();

                if (count < 1)
                    throw new InvalidOperationException("The minimum number of columns is 1.");

                if (count > 10)
                    throw new InvalidOperationException("The maximum number of columns is 10.");

                _columns = value;
            }
        }

        /// <summary>
        /// Gets or sets the aspect ratio of the image.
        /// </summary>
        [JsonProperty("imageAspectRatio")]
        [JsonConverter(typeof(EnumConverter<ImageAspectRatio>))]
        public ImageAspectRatio ImageAspectRatio { get; set; } = ImageAspectRatio.Rectangle;

        /// <summary>
        /// Gets or sets the size of the image.
        /// </summary>
        [JsonProperty("imageSize")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        public ImageSize ImageSize { get; set; } = ImageSize.Cover;

        void ITemplate.Validate()
        {
            if (_columns is null)
                throw new InvalidOperationException("The columns cannot be null.");

            _columns.Validate();
        }
    }
}
