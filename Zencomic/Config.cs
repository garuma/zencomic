// 
// Config.cs
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
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Zencomic
{
	
	public class Config
	{
		public enum PopupMethod
		{
			Notification,
			Window
		}
		
		static readonly string basePath
			= Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData), "Zencomic");
		
		static readonly string path = Path.Combine (basePath, "config.xml");
		
		bool launchOnStartup;
		int showDelay;
		int popupTime;
		Config.PopupMethod popupMethod;
		
		public bool LaunchOnStartup {
			get {
				return launchOnStartup;
			}
			set {
				launchOnStartup = value;
			}
		}
		
		public int ShowDelay {
			get {
				return showDelay;
			}
			set {
				showDelay = value;
				if (ShowDelayChanged != null)
					ShowDelayChanged (this, EventArgs.Empty);
			}
		}
		
		public static string ConfigPath {
			get {
				return basePath;
			}
		}

		public int PopupTime {
			get {
				return popupTime;
			}
			set {
				popupTime = value;
				if (PopupTimeChanged != null)
					PopupTimeChanged (this, EventArgs.Empty);
			}
		}
		
		public Config.PopupMethod Method {
			get {
				return popupMethod;
			}
			set {
				if (value != popupMethod) {
					popupMethod = value;
					if (PopupMethodChanged != null)
						PopupMethodChanged (this, EventArgs.Empty);
				}
			}
		}
		
		public event EventHandler ShowDelayChanged;
		public event EventHandler PopupTimeChanged;
		public event EventHandler PopupMethodChanged;
		
		public Config ()
		{
			this.launchOnStartup = false;
			this.showDelay = 5;
			this.popupTime = 30;
			this.popupMethod = PopupMethod.Notification;
		}
		
		public static Config RestoreSaved ()
		{
			XmlSerializer serializer = new XmlSerializer (typeof (Config));
		
			if (!File.Exists (path))
				return new Config ();
			
			Stream configFile = File.OpenRead (path);
			Config config = (Config)serializer.Deserialize (configFile);
			configFile.Close ();
			
			return config;
		}
		
		public void Save ()
		{
			Stream configFile = File.Open (path, FileMode.Create, FileAccess.Write);
			XmlSerializer serializer = new XmlSerializer (typeof (Config));
			serializer.Serialize (configFile, this);
			configFile.Close ();
		}
	}
}
