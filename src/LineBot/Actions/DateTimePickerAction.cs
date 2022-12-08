// Copyright Dirk Lemstra (https://github.com/dlemstra/line-bot-sdk-dotnet).
// Licensed under the Apache License, Version 2.0.

using System;
using Newtonsoft.Json;

namespace Line
{
    /// <summary>
    /// Encapsulates a datetimepicker action.
    /// </summary>
    public sealed class DateTimePickerAction : IAction
    {
        [JsonProperty("mode")]
        [JsonConverter(typeof(EnumConverter<DateTimePickerMode>))]
        private readonly DateTimePickerMode _mode;

        private string? _label;
        private string? _data;
        private DateTime? _initial;
        private DateTime? _min;
        private DateTime? _max;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimePickerAction"/> class.
        /// </summary>
        /// <param name="mode">The mode of the Datetime picker action.</param>
        public DateTimePickerAction(DateTimePickerMode mode)
            => _mode = mode;

        internal DateTimePickerAction()
        {
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(EnumConverter<ActionType>))]
        ActionType IAction.Type
            => ActionType.DateTimePicker;

        /// <summary>
        /// Gets or sets the string returned via webhook in the postback.data property of the <see cref="IPostback"/> event.
        /// <para>Max: 300 characters.</para>
        /// </summary>
        [JsonProperty("data")]
        public string? Data
        {
            get => _data;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The data cannot be null or whitespace.");

                if (value.Length > 300)
                    throw new InvalidOperationException("The data cannot be longer than 300 characters.");

                _data = value;
            }
        }

        /// <summary>
        /// Gets or sets the initial value of date or time for the datetime picker.
        /// </summary>
        [JsonIgnore]
        public DateTime? Initial
        {
            get => _initial;
            set
            {
                var adjustedValue = AdjustedDateTimeByMode(value);
                if ((Min.HasValue && adjustedValue < Min.Value) || (Max.HasValue && adjustedValue > Max.Value))
                    throw new InvalidOperationException("The initial must be between the min and the max.");

                _initial = adjustedValue;
            }
        }

        /// <summary>
        /// Gets or sets the label.
        /// <para>Max: 20 characters.</para>
        /// </summary>
        [JsonProperty("label")]
        public string? Label
        {
            get => _label;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("The label cannot be null or whitespace.");

                if (value.Length > 20)
                    throw new InvalidOperationException("The label cannot be longer than 20 characters.");

                _label = value;
            }
        }

        /// <summary>
        /// Gets or sets the largest date or time value that can be selected for the datetime picker.
        /// </summary>
        [JsonIgnore]
        public DateTime? Max
        {
            get => _max;
            set
            {
                var adjustedValue = AdjustedDateTimeByMode(value);
                if (Min.HasValue && adjustedValue <= Min.Value)
                    throw new InvalidOperationException("The max must be greater than the min.");

                _max = adjustedValue;
            }
        }

        /// <summary>
        /// Gets or sets the smallest date or time value that can be selected for the datetime picker.
        /// </summary>
        [JsonIgnore]
        public DateTime? Min
        {
            get => _min;
            set
            {
                var adjustedValue = AdjustedDateTimeByMode(value);
                if (Max.HasValue && adjustedValue >= Max.Value)
                    throw new InvalidOperationException("The min must be less than the max.");

                _min = adjustedValue;
            }
        }

        /// <summary>
        /// Gets the action mode for the datetime picker.
        /// </summary>
        [JsonIgnore]
        public DateTimePickerMode Mode
            => _mode;

        /// <summary>
        /// Gets or sets the formatted initial value.
        /// </summary>
        [JsonProperty("initial")]
        private string InitialFormatted
        {
            get => GetFormattedDateTimeByMode(_initial);
            set
            {
                if (string.IsNullOrEmpty(value))
                    Initial = null;
                else
                    Initial = DateTime.Parse(value);
            }
        }

        /// <summary>
        /// Gets or sets the formatted max value.
        /// </summary>
        [JsonProperty("max")]
        private string MaxFormatted
        {
            get => GetFormattedDateTimeByMode(_max);
            set
            {
                if (string.IsNullOrEmpty(value))
                    Max = null;
                else
                    Max = DateTime.Parse(value);
            }
        }

        /// <summary>
        /// Gets or sets the formatted min value.
        /// </summary>
        [JsonProperty("min")]
        private string MinFormatted
        {
            get => GetFormattedDateTimeByMode(_min);
            set
            {
                if (string.IsNullOrEmpty(value))
                    Min = null;
                else
                    Min = DateTime.Parse(value);
            }
        }

        void IAction.Validate()
        {
            if (_label is null)
                throw new InvalidOperationException("The label cannot be null.");

            if (_data is null)
                throw new InvalidOperationException("The data cannot be null.");
        }

        private string GetFormattedDateTimeByMode(DateTime? dateTime)
        {
            if (dateTime is null)
                return string.Empty;
            if (Mode == DateTimePickerMode.Date)
                return dateTime.Value.ToString("yyyy-MM-dd");
            else if (Mode == DateTimePickerMode.Time)
                return dateTime.Value.ToString("HH:mm");
            else
                return dateTime.Value.ToString("yyyy-MM-ddTHH:mm");
        }

        private DateTime? AdjustedDateTimeByMode(DateTime? value)
        {
            if (value is null)
                return null;

            var adjustedDateTime = value.Value;

            if (Mode == DateTimePickerMode.Date)
                adjustedDateTime = new DateTime(adjustedDateTime.Year, adjustedDateTime.Month, adjustedDateTime.Day, 0, 0, 0);
            else if (Mode == DateTimePickerMode.Time)
                adjustedDateTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, adjustedDateTime.Hour, adjustedDateTime.Minute, adjustedDateTime.Second);

            return adjustedDateTime;
        }
    }
}
