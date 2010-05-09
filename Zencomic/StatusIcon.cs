// 
// CprStatusIcon.cs
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
using Mono.Unix;

using Gtk;
using Gdk;

namespace Zencomic
{
	public class CprStatusIcon : Gtk.StatusIcon
	{
		CheckMenuItem activated;
		Menu menu;
		
		Func<PreferencesDialog> dialogCreator;

		PreferencesDialog dialog;
		
		INotificationService notifications = new RealWindowNotifications ();
		IScreenSaver screensaver = ScreensaverService.GetScreensaver ();
		
		uint lastId;
		int delay = 5;
		
		public CprStatusIcon (Func<PreferencesDialog> dialogCreator) 
			: base (Pixbuf.LoadFromResource ("Zencomic.data.dilbert.png"))
		{
			this.dialogCreator = dialogCreator;
			this.PopupMenu += PopupMenuHandler;
			SetupMenu ();
			AddTimeout (delay);
			
			if (screensaver != null) {
				screensaver.ActiveChanged += delegate (bool active) {
					RemoveTimeout ();
					if (active)
						AddTimeout (delay);
				};
			}
		}
		
		void SetupMenu ()
		{
			activated = new CheckMenuItem (Catalog.GetString ("Activate"));
			menu = new Menu ();
			activated.Active = true;
			
			MenuItem now = new MenuItem (Catalog.GetString ("Show now"));
			SeparatorMenuItem separator = new SeparatorMenuItem ();
			MenuItem preferences = new ImageMenuItem (Gtk.Stock.Preferences, null);
			MenuItem quit = new ImageMenuItem (Gtk.Stock.Quit, null);
			
			quit.Activated += QuitAction;
			preferences.Activated += PreferencesActivated;
			activated.Activated += EnableActivated;
			now.Activated += ShowNowActivated;
			
			menu.Add (activated);
			menu.Add (now);
			menu.Add (separator);
			menu.Add (preferences);
			menu.Add (quit);
		}
		
		void PopupMenuHandler (object sender, PopupMenuArgs e)
		{
			menu.ShowAll ();
			menu.Popup ();
		}
		
		void AddTimeout (int interval)
		{
#if USE_GLIB_TIMEOUT_SECONDS
			lastId = GLib.Timeout.AddSeconds (((uint)interval) * 60, IdleHandlerMethod);
#else
			lastId = GLib.Timeout.Add (((uint)interval) * 1000 * 60, IdleHandlerMethod);
#endif
		}
		
		void RemoveTimeout ()
		{
			GLib.Source.Remove (lastId);
		}

		void PreferencesActivated (object sender, EventArgs e)
		{
			dialog = dialogCreator ();
			
			dialog.ShowAll ();
			dialog.KeepAbove = true;
			dialog.AcceptFocus = true;
			dialog.SetPosition (Gtk.WindowPosition.Center);
			dialog.GrabFocus ();
			
			dialog.Present ();
			dialog.Run ();
			dialog.Destroy ();
			
			dialog = null;
		}
		
		void EnableActivated (object sender, EventArgs e)
		{
			CheckMenuItem item = sender as CheckMenuItem;
			if (item == null)
				return;
			
			RemoveTimeout ();
			if (item.Active)
				AddTimeout (delay);
		}
		
		void ShowNowActivated (object sender, EventArgs e)
		{
			IdleHandlerMethod ();
		}
		
		public bool ProcessingActivated {
			get {
				return activated.Active;
			}
		}
		
		public int ShowDelay {
			set {
				if (value == 0)
					return;
				delay = value;
				
				if (activated.Active) {
					RemoveTimeout ();
					AddTimeout (delay);
				}
			}
		}
		
		public int PopupTime {
			set {
				notifications.PopupDelay = value;
			}
		}
		
		public Config.PopupMethod Method {
			set {
				switch (value) {
				case Config.PopupMethod.Notification:
					this.notifications = new Notifications ();
					break;
				case Config.PopupMethod.Window:
					this.notifications = new RealWindowNotifications ();
					break;
				default:
					this.notifications = new Notifications ();
					break;
				}
			}
		}
		
		void QuitAction (object sender, EventArgs e)
		{
			Application.Quit ();
		}
		
		bool IdleHandlerMethod ()
		{
			if (screensaver != null && screensaver.GetActive ())
				return false;
				
			ComicService.GetNextComic (notifications.Notification);
							
			return true;
		}
	}
}
