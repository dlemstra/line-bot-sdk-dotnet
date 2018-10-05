using System;
using Newtonsoft.Json;

namespace Line
{
	/// <summary>
	/// Encapsulates a template camera action.
	/// </summary>
	public sealed class CameraAction : IAction
	{
#pragma warning disable 0414 // Suppress value is never used.
		[JsonProperty("type")]
		[JsonConverter(typeof(EnumConverter<ActionType>))]
		private readonly ActionType _type = ActionType.Camera;
#pragma warning restore 0414

		private string _label;

		/// <summary>
		/// Gets or sets the label.
		/// <para>Max: 20 characters.</para>
		/// </summary>
		public string Label
		{
			get
			{
				return _label;
			}

			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new InvalidOperationException("The label cannot be null or whitespace.");

				if (value.Length > 20)
					throw new InvalidOperationException("The label cannot be longer than 20 characters.");

				_label = value;
			}
		}


		public void Validate()
		{
			if (_label == null)
				throw new InvalidOperationException("The label cannot be null.");
		}
	}
}
