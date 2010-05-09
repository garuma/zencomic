// 
// PreferencesDialog.cs
//  
// Author:
//       Jérémie "Garuma" Laval <jeremie.laval@gmail.com>
// 
// Copyright (c) 2009 Jérémie "Garuma" Laval
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;

using Gtk;

using Mono.Addins;
using Mono.Addins.Gui;

using ZencomicLib;

namespace Zencomic
{
	public partial class PreferencesDialog : Gtk.Dialog
	{
		Config config;
		ListStore store = new ListStore (typeof (string), typeof (bool), typeof (Addin));

		public PreferencesDialog (Config config)
			: base ("Zencomic preferences", null, DialogFlags.Modal)
		{
			this.config = config;
			
			this.Build();
			this.panelIcon.SetFromStock (Stock.Preferences, IconSize.Dialog);
			
			InitFromConfig ();
			BuildComicList ();
		}
		
		void InitFromConfig ()
		{
			showDelayButton.Value = config.ShowDelay;
			popupDelayButton.Value = config.PopupTime;
			radiobutton2.Active = config.Method == Config.PopupMethod.Notification;
			radiobutton3.Active = config.Method == Config.PopupMethod.Window;
		}
		
		void BuildComicList ()
		{
			comicTv.Model = store;
			
			comicTv.AppendColumn ("Name", new CellRendererText (), "text", 0);
			
			CellRendererToggle cellToggle = new CellRendererToggle ();
			cellToggle.Activatable = true;
			cellToggle.Toggled += OnCellRendererToggled;
			comicTv.AppendColumn ("Enabled", cellToggle, "active", 1);
			
			foreach (Addin addin in AddinManager.Registry.GetAddins ()) {
				store.AppendValues (addin.LocalId, addin.Enabled, addin);
			}
		}

		protected virtual void OnShowDelayButtonValueChanged (object sender, System.EventArgs e)
		{
			SpinButton b = sender as SpinButton;
			if (b == null)
				return;
			
			config.ShowDelay = (int)b.Value;
		}
		
		protected virtual void OnPopupDelayButtonValueChanged (object sender, System.EventArgs e)
		{
			SpinButton b = sender as SpinButton;
			if (b == null)
				return;
			
			config.PopupTime = (int)b.Value;
		}
		
		protected virtual void OnButton75Clicked (object sender, System.EventArgs e)
		{
			AddinManagerWindow.Run (this);
		}

		protected virtual void OnRadiobutton2Toggled (object sender, System.EventArgs e)
		{
			config.Method = Config.PopupMethod.Notification;
		}

		protected virtual void OnRadiobutton3Toggled (object sender, System.EventArgs e)
		{
			config.Method = Config.PopupMethod.Window;
		}
		
		protected virtual void OnCellRendererToggled (object sender, ToggledArgs e)
		{
			TreeIter iter;
			
			if (store.GetIter (out iter, new TreePath(e.Path))) {
				Addin addin = store.GetValue (iter, 2) as Addin;
				bool old = (bool)store.GetValue (iter, 1);
				
				if (addin != null) {
					addin.Enabled = !old;
					store.SetValue (iter, 1, !old);
				}
			}
		}
	}
}
