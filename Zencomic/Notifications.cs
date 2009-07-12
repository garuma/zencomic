// 
// Notifications.cs
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

/* The RealWindowNotifications stuff is heavily based on Aaron Bockover
 * Banshee's ArtworkPopup code.
 */

using System;
using System.Collections.Generic;

using Gtk;
using Gdk;

using NDesk.DBus;

namespace Zencomic
{
	[Interface ("org.freedesktop.Notifications")]
	public interface INotifications {
		uint Notify (string app_name, uint id, string icon, string summary, string body,
			string[] actions, IDictionary<string, object> hints, int timeout);
	}
	
	public interface INotificationService
	{
		void Notification (Pixbuf image, string name, string author);
		int PopupDelay { set; }
	}
	
	public class RealWindowNotifications : INotificationService
	{
		class PopupWindow
		{
			int popupDelay = 20;
			
			Gtk.Image image;
			Label label;
			VBox vbox;
			Gtk.Window win = new Gtk.Window (Gtk.WindowType.Popup);
			
			public PopupWindow ()
			{
				Color bg = new Color (85, 87, 83);
				Color fg = new Color (211, 215, 207);
				
				EventBox box = new EventBox ();
				vbox = new VBox();
								
				box.ButtonPressEvent += HandleButtonEvent;
				
				box.Add (vbox);
	            
	            win.Decorated = false;
	            win.BorderWidth = 6;
				
	            
	            win.SetPosition(WindowPosition.CenterAlways);

				image = new Gtk.Image ();
				label = new Label ();
	            label.CanFocus = false;
	            label.Wrap = true;           
				
				win.Add(box);
	            
	            win.ModifyBg (StateType.Normal, bg);
				label.ModifyFg (StateType.Normal, fg);
				box.ModifyBg (StateType.Normal, bg);
				
	            vbox.PackStart(image, true, true, 0);
	            vbox.PackStart(label, false, false, 0);
	            
	            vbox.Spacing = 6;
				box.ShowAll ();
			}
			
			public void Notification (Pixbuf pixbuf, string name, string author)
			{
				var screen = win.Screen;
				
				const int diviser = 2;
				
				int desiredWidth = pixbuf.Width;
				int desiredHeight = pixbuf.Height;
				
				if (pixbuf.Width > screen.Width / diviser) {
					desiredWidth = screen.Width / diviser;
					desiredHeight = desiredHeight * desiredWidth / pixbuf.Width;
				}
				if (pixbuf.Height > screen.Height / diviser) {
					desiredHeight = screen.Height / diviser;
					desiredWidth = desiredWidth * desiredHeight / pixbuf.Height;
				}
				
				if (pixbuf.Width != desiredWidth || pixbuf.Height != desiredHeight)
					pixbuf = pixbuf.ScaleSimple (desiredWidth, desiredHeight, InterpType.Bilinear);
				
				image.Pixbuf = pixbuf;
				label.Markup = string.Format ("<b>{0}</b> of {1}",
				                              GLib.Markup.EscapeText(name),
				                              GLib.Markup.EscapeText (author));
				
				image.SetSizeRequest(image.Pixbuf.Width, image.Pixbuf.Height);
				label.SetSizeRequest(-1, -1);
				
				int text_w, text_h;
				label.Layout.GetPixelSize(out text_w, out text_h);
				if(image.Pixbuf.Width < text_w) {
					label.SetSizeRequest(image.Pixbuf.Width, -1);
				} 
				
				win.Resize(1, 1);

				win.ShowAll ();

#if USE_GLIB_TIMEOUT_SECONDS
				GLib.Timeout.AddSeconds (((uint)popupDelay), HideAllCallback);
#else
				GLib.Timeout.Add (((uint)popupDelay) * 1000, HideAllCallback);
#endif
			}
			
			public int PopupDelay {
				set {
					popupDelay = value;
				}
			}
			
			void HandleButtonEvent (object sender, ButtonPressEventArgs e)
			{
				HideAllCallback ();
			}
			
			bool HideAllCallback ()
			{
				if (!win.Visible)
					return false;

				win.HideAll ();
				
				Gdk.Pixbuf p = image.Pixbuf;
				image.Pixbuf = null;
				if (p != null)
					p.Dispose ();
				
				return false;
			}
		}
		
		int popupDelay = 20;
		
		#region INotificationService implementation
		public void Notification (Pixbuf image, string name, string author)
		{
			Application.Invoke (delegate {
				PopupWindow window = new PopupWindow ();
				window.PopupDelay = popupDelay;
				
				window.Notification (image, name, author);
			});
		}
		
		public int PopupDelay {
			set {
				this.popupDelay = value;
			}
		}
		#endregion
	}
	
	public class Notifications : INotificationService
	{
		int popupDelay = 20;
		
		struct IconData {
			public int Width;
			public int Height;
			public int Rowstride;
			public bool HasAlpha;
			public int BitsPerSample;
			public int NChannels;
			public byte[] Pixels;		
		}
		
		const string busName = "org.freedesktop.Notifications";
		const string objectPath = "/org/freedesktop/Notifications";
		
		INotifications proxy;
		
		public Notifications ()
		{
			if (!Bus.Session.NameHasOwner (busName))
				Bus.Session.StartServiceByName (busName, 0);
			
			proxy =  Bus.Session.GetObject<INotifications> (busName, new ObjectPath (objectPath));
		}
		
		public void Notification (Pixbuf image, string name, string author)
		{
			const int maxWidth = 387;
			Pixbuf pixbuf = image.ScaleSimple (maxWidth, image.Height * maxWidth / image.Width, InterpType.Hyper);
			image.Dispose ();
			
			IDictionary<string, object> hints = new Dictionary<string, object> ();
			IconData icon_data = new IconData ();
			icon_data.Width = pixbuf.Width;
			icon_data.Height = pixbuf.Height;
			icon_data.Rowstride = pixbuf.Rowstride;
			icon_data.HasAlpha = pixbuf.HasAlpha;
			icon_data.BitsPerSample = pixbuf.BitsPerSample;
			icon_data.NChannels = pixbuf.NChannels;

			int len = (icon_data.Height - 1) * icon_data.Rowstride + icon_data.Width *
				((icon_data.NChannels * icon_data.BitsPerSample + 7) / 8);
			icon_data.Pixels = new byte[len];
			System.Runtime.InteropServices.Marshal.Copy (pixbuf.Pixels, icon_data.Pixels, 0, len);

			hints["icon_data"] = icon_data;
			
			int x = Gdk.Screen.Default.Width / 2;
			int y = 0;
			hints["x"] = x;
			hints["y"] = y;
			
			proxy.Notify ("zencomic", 0, string.Empty, name + " of " + author, 
			              string.Empty, new string[0], hints, popupDelay * 1000);
		}
		
		public int PopupDelay {
			set {
				popupDelay = value;
			}
		}
	}
}
