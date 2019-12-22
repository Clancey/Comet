﻿using System.Windows;

namespace Comet.WPF.Handlers
{
	public class ContentViewHandler : AbstractHandler<ContentView, UIElement>
    {
        protected override UIElement CreateView()
        {
            return VirtualView?.Content.ToView();
        }
    }
}
