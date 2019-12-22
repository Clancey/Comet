﻿using AppKit;
using Comet.Mac;
using System.Drawing;
using Bitmap = Comet.Graphics.Bitmap;

namespace Comet.Map.Graphics
{
	public class NSImageBitmap : Bitmap
    {
        private NSImage _image;

        public NSImageBitmap(NSImage image)
        {
            _image = image;
        }

        public override SizeF Size => _image?.Size.ToSizeF() ?? SizeF.Empty;

        public override object NativeBitmap => _image;

        protected override void DisposeNative()
        {
            _image?.Dispose();
            _image = null;
        }
    }
}
