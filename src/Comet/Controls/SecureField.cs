﻿using System;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;

namespace Comet
{
	public class SecureField : View, IEntry
	{
		public SecureField(
			Binding<string> value = null,
			Binding<string> placeholder = null,
			Action<string> onCommit = null)
		{
			Text = value;
			Placeholder = placeholder;
			OnCommit = onCommit;
		}

		public SecureField(
			Func<string> value,
			Func<string> placeholder = null,
			Action<string> onCommit = null) : this((Binding<string>)value, (Binding<string>)placeholder, onCommit)
		{ }

		Binding<string> _text;
		public Binding<string> Text
		{
			get => _text;
			private set => this.SetBindingValue(ref _text, value);
		}


		Binding<string> _placeholder;
		public Binding<string> Placeholder
		{
			get => _placeholder;
			private set => this.SetBindingValue(ref _placeholder, value);
		}

		public Action<string> OnCommit { get; set; }


		bool IEntry.IsPassword => true;

		//TODO: Expose these properties
		bool IEntry.IsTextPredictionEnabled => this.GetEnvironment<bool>(nameof(IEntry.IsTextPredictionEnabled));

		ReturnType IEntry.ReturnType => this.GetEnvironment<ReturnType>(nameof(IEntry.ReturnType));

		ClearButtonVisibility IEntry.ClearButtonVisibility => this.GetEnvironment<ClearButtonVisibility>(nameof(IEntry.ClearButtonVisibility));

		string ITextInput.Text { get => Text; set => Text.Set(value); }

		bool ITextInput.IsReadOnly => this.GetEnvironment<bool>(nameof(IEntry.IsReadOnly));

		int ITextInput.MaxLength => this.GetEnvironment<int>(nameof(IEntry.MaxLength));

		string IText.Text => Text;

		Color IText.TextColor => this.GetColor(null);

		Font IText.Font => this.GetFont(null);

		double IText.CharacterSpacing => this.GetEnvironment<double>(nameof(IText.CharacterSpacing));

		string IPlaceholder.Placeholder => this.Placeholder;

		TextAlignment ITextAlignment.HorizontalTextAlignment => this.GetTextAlignment() ?? TextAlignment.Start;

		public void ValueChanged(string value)
			=> OnCommit?.Invoke(value);
	}
}
