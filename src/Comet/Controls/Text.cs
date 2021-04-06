﻿using System;

using Microsoft.Maui;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;

namespace Comet
{
	/// <summary>
	/// A view that displays one or more lines of read-only text.
	/// </summary>
	public class Text : View, ILabel
	{
		public Text(
			Binding<string> value = null)
		{
			Value = value;
		}
		public Text(
			Func<string> value) : this((Binding<string>)value)
		{

		}

		Binding<string> _value;
		public Binding<string> Value
		{
			get => _value;
			private set => this.SetBindingValue(ref _value, value);
		}

		public override string ToString() => _value?.Value?.ToString() ?? string.Empty;

		string IText.Text => Value?.CurrentValue;


		Font IText.Font => this.GetFont(null);


		double IText.CharacterSpacing => this.GetEnvironment<double>(nameof(IText.CharacterSpacing));


		TextAlignment ITextAlignment.HorizontalTextAlignment => this.GetTextAlignment() ?? TextAlignment.Start;

		//TextAlignment ITextAlignment.VerticalTextAlignment => this.GetVerticalTextAlignment() ?? TextAlignment.Start;

		Microsoft.Maui.LineBreakMode ILabel.LineBreakMode => this.GetLineBreakMode(LineBreakMode.NoWrap);

		int ILabel.MaxLines => this.GetEnvironment<int>(nameof(ILabel.MaxLines));

		TextDecorations ILabel.TextDecorations => this.GetEnvironment<TextDecorations>(nameof(ILabel.TextDecorations));

		double ILabel.LineHeight => this.GetEnvironment<double>(nameof(ILabel.LineHeight));

		Color IText.TextColor => this.GetColor();

		Thickness IPadding.Padding => this.GetPadding();
	}
}
