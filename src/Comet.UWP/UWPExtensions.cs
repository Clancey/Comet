﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UWPTextAlignment = Windows.UI.Xaml.TextAlignment;

namespace Comet.UWP
{
	public static class UWPExtensions
	{
		static UWPExtensions()
		{
			UI.Init();
		}

		public static UWPViewHandler GetOrCreateViewHandler(this View view)
		{
			if (view == null)
				return null;
			var handler = view.ViewHandler;
			if (handler == null)
			{
				handler = Registrar.Handlers.GetHandler(view.GetType());
				view.ViewHandler = handler;
			}

			var iUIElement = handler as UWPViewHandler;
			return iUIElement;
		}

		public static UIElement ToView(this View view)
		{
			var handler = view.GetOrCreateViewHandler();
			return handler?.View;
		}

		public static UIElement ToEmbeddableView(this View view)
		{
			var handler = view.GetOrCreateViewHandler();
			if (handler == null)
				throw new Exception("Unable to build handler for view");

			if (handler is FrameworkElement element)
			{
				if (element.Parent is CometView container)
					return container;
			}

			return new CometView(view);
		}

		public static void RemoveChild(this DependencyObject parent, UIElement child)
		{
			if (parent is Panel panel)
			{
				panel.Children.Remove(child);
				return;
			}

			/*var decorator = parent as Decorator;
            if (decorator != null)
            {
                if (decorator.Child == child)
                {
                    decorator.Child = null;
                }
                return;
            }*/

			if (parent is ContentPresenter contentPresenter)
			{
				if (contentPresenter.Content == child)
				{
					contentPresenter.Content = null;
				}
				return;
			}

			if (parent is ContentControl contentControl)
			{
				if (contentControl.Content == child)
				{
					contentControl.Content = null;
				}
				return;
			}
		}

		public static UWPTextAlignment ToTextAlignment(this TextAlignment? target)
		{
			if (target == null)
				return UWPTextAlignment.Start;

			switch (target)
			{
				case TextAlignment.Natural:
					return UWPTextAlignment.Start;
				case TextAlignment.Left:
					return UWPTextAlignment.Left;
				case TextAlignment.Right:
					return UWPTextAlignment.Right;
				case TextAlignment.Center:
					return UWPTextAlignment.Center;
				case TextAlignment.Justified:
					return UWPTextAlignment.Justify;
				default:
					throw new ArgumentOutOfRangeException(nameof(target), target, null);
			}
		}
	}
}
