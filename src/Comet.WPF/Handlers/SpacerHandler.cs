﻿
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

using System.Windows.Controls;

namespace Comet.WPF.Handlers
{
    public class SpacerHandler : AbstractHandler<Spacer, Canvas>
    {
        protected override Canvas CreateView()
        {
            return new Canvas();
        }
    }
}
