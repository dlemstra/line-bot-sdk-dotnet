// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a image carousel template.
    /// </summary>
    public sealed class ImageCarouselTemplate : ITemplate
    {
        private IEnumerable<ImageCarouselColumn>? _columns = null;

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<TemplateType>))]
        TemplateType ITemplate.Type
            => TemplateType.Image_Carousel;

        /// <summary>
        /// Gets or sets the columns.
        /// <para>Max: 10.</para>
        /// </summary>
        [JsonProperty("columns")]
        public IEnumerable<ImageCarouselColumn>? Columns
        {
            get => _columns;
            set
            {
                if (value is null)
                    throw new InvalidOperationException("The columns cannot be null.");

                var count = value.Count();

                if (count < 1)
                    throw new InvalidOperationException("The minimum number of columns is 1.");

                if (count > 10)
                    throw new InvalidOperationException("The maximum number of columns is 10.");

                _columns = value;
            }
        }

        void ITemplate.Validate()
        {
            if (_columns is null)
                throw new InvalidOperationException("The columns cannot be null.");

            _columns.Validate();
        }
    }
}
