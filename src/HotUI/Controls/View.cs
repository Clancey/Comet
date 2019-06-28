﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HotUI {

	public class View {
		View parent;
		public View Parent {
			get => parent;
			set {
				if (parent == value)
					return;
				parent = value;
				OnParentChange (value);
			}
		}
		protected virtual void OnParentChange(View parent)
		{
			this.Navigation = parent.Navigation ?? parent as NavigationView;
		}
		public NavigationView Navigation { get; set; }
		protected State State { get; set; }
		public View (bool hasConstructors)
		{
			State = StateBuilder.CurrentState ?? new State {
				StateChanged = ResetView
			};
			if(!hasConstructors)
				State.StartBuildingView ();

		}
		public View () : this (false)
		{
		}


		IViewHandler viewHandler;
		public IViewHandler ViewHandler {
			get => viewHandler;
			set {
				if (viewHandler == value)
					return;
				viewHandler?.Remove (this);
				viewHandler = value;
				WillUpdateView ();
				viewHandler?.SetView (this.GetRenderView());
			}
		}
		internal void UpdateFromOldView (IViewHandler handler) => viewHandler = handler;
		View builtView;
		public View BuiltView => builtView;
		void ResetView()
		{
			builtView = null;
			if (ViewHandler == null)
				return;
			ViewHandler.Remove (this);
			WillUpdateView ();
			ViewHandler?.SetView (this.GetRenderView ());
		}

		Func<View> body;
		public Func<View> Body {
			get => body;
			set => this.SetValue(State,ref body, value, (s,o)=> ResetView());
		}
		internal View GetView () => GetRenderView ();

		protected virtual View GetRenderView ()
		{
			if (Body == null)
				return this;
			if (builtView != null)
				return builtView;
			using (new StateBuilder (State)) {
				State.SetParent (this);
				State.StartProperty ();
				var view = Body.Invoke ();
				view.Parent = this.Parent;
				var props = State.EndProperty ();
				var propCount = props.Length;
				if (propCount > 0) {
					State.BindingState.AddViewProperty (props, (s, o) => ResetView ());
				}
				return builtView = view;
			}
		}

		protected virtual void WillUpdateView ()
		{

		}
		protected void ViewPropertyChanged (string property, object value)
		{
			//These views are destroyed and not used again. We keep this on in Debug, so we can easily write tests to check the value changed
#if DEBUG
			this.SetPropertyValue (property, value);
#endif
			ViewHandler?.UpdateValue (property, value);
		}
	}



	public enum LayoutOptions {
		Start,
		Center,
		End,
		Fill,
		StartAndExpand,
		CenterAndExpand,
		EndAndExpand,
		FillAndExpand
	}
}
