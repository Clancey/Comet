﻿using Comet.Skia.UWP;

namespace Comet.Skia
{
	public static class UI
	{
		static bool _hasInitialized;

		public static void Init()
		{
			if (_hasInitialized) return;
			_hasInitialized = true;

			Comet.UWP.UI.Init();
			// Controls
			Registrar.Handlers.Register<DrawableControl, DrawableControlHandler>();
			Registrar.Handlers.Register<SkiaView, SkiaViewHandler>();

			var generic = typeof(SkiaControlHandler<>);
			Skia.Internal.Registration.RegisterDefaultViews(generic);
		}
	}
}
