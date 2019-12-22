﻿using System;

namespace Comet
{
	public class Button : View
	{
		public Button(
			Binding<string> text = null,
			Action action = null)
		{
			Text = text;
			OnClick = action;
		}

		public Button(
			Func<string> text,
			Action action = null) : this((Binding<string>)text, action)
		{

		}

		Binding<string> _text;
		public Binding<string> Text
		{
			get => _text;
			private set => this.SetBindingValue(ref _text, value);
		}

		public Action OnClick { get; private set; }
	}
}
