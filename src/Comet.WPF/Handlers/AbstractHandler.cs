using System;
using System.Windows;
using System.Windows.Controls;
using Comet.WPF.Controls;
using System.Drawing;


namespace Comet.WPF.Handlers
{
	public abstract class AbstractHandler<TVirtualView, TNativeView> : WPFViewHandler
		where TVirtualView : View
		where TNativeView : UIElement
	{
		protected readonly PropertyMapper<TVirtualView> mapper;

		protected AbstractHandler(PropertyMapper<TVirtualView> mapper)
		{
			this.mapper = mapper;
		}

		protected AbstractHandler()
		{

		}


		private TVirtualView _virtualView;
		private TNativeView _nativeView;

		public event EventHandler<ViewChangedEventArgs> NativeViewChanged;

		protected abstract TNativeView CreateView();

		public UIElement View => _nativeView;

		public CUIContainerView ContainerView => null;

		public object NativeView => _nativeView;

		public TNativeView TypedNativeView => _nativeView;

		protected TVirtualView VirtualView => _virtualView;

		public virtual void SetView(View view)
		{
			_virtualView = (TVirtualView)view;
			if (_nativeView == null)
				_nativeView = CreateView();
			mapper?.UpdateProperties(this, _virtualView);
		}

		public virtual void Remove(View view)
		{
			_virtualView = null;
		}

		protected virtual void DisposeView(TNativeView nativeView)
		{

		}

		public virtual void UpdateValue(string property, object value)
		{
			mapper?.UpdateProperty(this, _virtualView, property);
		}

		public bool HasContainer
		{
			get => false;
			set { }
		}

		public virtual SizeF GetIntrinsicSize(SizeF availableSize)
		{
			_nativeView?.Measure(availableSize.ToWSize());
			return _nativeView?.DesiredSize.ToSizeF() ?? availableSize;
		}

		public void SetFrame(RectangleF frame)
		{
			_nativeView?.Arrange(frame.ToRect());
		}

		protected void BroadcastNativeViewChanged(UIElement previousView, UIElement newView)
		{
			NativeViewChanged?.Invoke(this, new ViewChangedEventArgs(VirtualView, previousView, newView));
		}

		#region IDisposable Support
		private bool _disposed = false; // To detect redundant calls

		private void Dispose(bool disposing)
		{
			if (!disposing)
				return;

			if (_nativeView is FrameworkElement element)
				element.Parent.RemoveChild(_nativeView);

			if (_nativeView != null)
				DisposeView(_nativeView);

			if (_nativeView is IDisposable disposable)
				disposable.Dispose();

			_nativeView = null;

			if (_virtualView != null)
				Remove(_virtualView);
		}

		void OnDispose(bool disposing)
		{
			if (_disposed)
				return;
			_disposed = true;
			Dispose(disposing);
		}

		~AbstractHandler()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			OnDispose(false);
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			OnDispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
