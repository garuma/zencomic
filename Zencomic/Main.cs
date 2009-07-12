// 
// Main.cs
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

using Gtk;
using Mono.Addins;
using Mono.Unix;
using NDesk.DBus;

using ZencomicLib;

namespace Zencomic
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Starting up");
			
			Application.Init ();
			BusG.Init ();
			InitSignals ();
			
			AddinManager.Initialize (Config.ConfigPath);
			AddinManager.Registry.Update (null);
			
			Config config = Config.RestoreSaved ();

			CprStatusIcon icon = new CprStatusIcon (() => new PreferencesDialog (config));
			icon.ShowDelay = config.ShowDelay;
			icon.Method = config.Method;
			icon.PopupTime = config.PopupTime;
			icon.Visible = true;
			
			config.ShowDelayChanged += delegate {
				icon.ShowDelay = config.ShowDelay;
			};
			config.PopupTimeChanged += delegate {
				icon.PopupTime = config.PopupTime;
			};
			config.PopupMethodChanged += delegate {
				icon.Method = config.Method;
				icon.PopupTime = config.PopupTime;
			};
			
			Application.Run ();
			
			Console.WriteLine ("Saving configuration and exiting");
			config.Save ();
		}
		
		static void InitSignals ()
		{
			UnixSignal term = new UnixSignal (Mono.Unix.Native.Signum.SIGTERM);
			UnixSignal inter = new UnixSignal (Mono.Unix.Native.Signum.SIGINT);
			
			GLib.Timeout.Add (500, delegate {
				if (term.IsSet || inter.IsSet)
					Application.Quit ();
				
				return true;
			});
		}
	}
}