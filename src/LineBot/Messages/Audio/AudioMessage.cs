// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates an audio message.
    /// </summary>
    public sealed class AudioMessage : ISendMessage
    {
        private Uri? _url;
        private int _duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMessage"/> class.
        /// </summary>
        public AudioMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMessage"/> class.
        /// </summary>
        /// <param name="url">
        /// The url of the audio file.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: M4A.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max duration: 1 minute.</para>
        /// <para>Max size: 10 MB.</para>
        /// </param>
        /// <param name="duration">The length of audio file in milliseconds.</param>
        public AudioMessage(string url, int duration)
            : this(new Uri(url), duration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioMessage"/> class.
        /// </summary>
        /// <param name="url">
        /// The url of the audio file.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: M4A.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max duration: 1 minute.</para>
        /// <para>Max size: 10 MB.</para>
        /// </param>
        /// <param name="duration">The length of audio file in milliseconds.</param>
        public AudioMessage(Uri url, int duration)
        {
            Url = url;
            Duration = duration;
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<MessageType>))]
        MessageType ISendMessage.Type
            => MessageType.Audio;

        /// <summary>
        /// Gets or sets the url of the audio file.
        /// <para>Protocol: HTTPS.</para>
        /// <para>Format: M4A.</para>
        /// <para>Max url length: 1000 characters.</para>
        /// <para>Max duration: 1 minute.</para>
        /// <para>Max size: 10 MB.</para>
        /// </summary>
        [JsonProperty("originalContentUrl")]
        public Uri? Url
        {
            get
            {
                return _url;
            }

            set
            {
                if (value is null)
                    throw new InvalidOperationException("The url cannot be null.");

                if (!"https".Equals(value.Scheme, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("The url should use the https scheme.");

                if (value.ToString().Length > 1000)
                    throw new InvalidOperationException("The url cannot be longer than 1000 characters.");

                _url = value;
            }
        }

        /// <summary>
        /// Gets or sets the length of audio file in milliseconds.
        /// </summary>
        [JsonProperty("duration")]
        public int Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                if (value < 1)
                    throw new InvalidOperationException("The duration should be at least 1 millisecond.");

                if (value >= 60000)
                    throw new InvalidOperationException("The duration cannot be longer than 1 minute.");

                _duration = value;
            }
        }

        void ISendMessage.Validate()
        {
            if (_url is null)
                throw new InvalidOperationException("The url cannot be null.");

            if (_duration == 0)
                throw new InvalidOperationException("The duration should be at least 1 millisecond.");
        }
    }
}
