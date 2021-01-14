﻿using System;
using System.Threading;
using Comet.Tests.Handlers;
using Xamarin.Platform;

namespace Comet.Tests
{
	public static class UI
	{
		static bool hasInit;
		public static void Init(bool force = false)
		{
			if (hasInit && !force)
				return;
			hasInit = true;
			Registrar.Handlers.Register<Button, GenericViewHandler>();
			Registrar.Handlers.Register<ContentView, GenericViewHandler>();
			Registrar.Handlers.Register<Image, GenericViewHandler>();
			Registrar.Handlers.Register<HStack, GenericViewHandler>();
			Registrar.Handlers.Register<ListView, GenericViewHandler>();
			Registrar.Handlers.Register<Text, TextHandler>();
			Registrar.Handlers.Register<TextField, TextFieldHandler>();
			Registrar.Handlers.Register<ProgressBar, ProgressBarHandler>();
			Registrar.Handlers.Register<SecureField, SecureFieldHandler>();
			Registrar.Handlers.Register<ScrollView, GenericViewHandler>();
			Registrar.Handlers.Register<Slider, SliderHandler>();
			Registrar.Handlers.Register<Toggle, GenericViewHandler>();
			Registrar.Handlers.Register<View, GenericViewHandler>();
			Registrar.Handlers.Register<VStack, GenericViewHandler>();
			Registrar.Handlers.Register<ZStack, GenericViewHandler>();

			ThreadHelper.Setup((a) => a?.Invoke());
			HotReloadHelper.IsEnabled = true;

			//Set Default Style
			var style = new Styles.Style();
			style.Apply();
		}
	}
}
