// 
// GarfieldMinusGarfieldComicAddin.cs
//  
// Author:
//       Alan McGovern <alan.mcgovern@gmail.com>
// 
// Copyright (c) 2010 Alan McGovern
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
using System.Net;
using System.Text.RegularExpressions;

using Gdk;
using Mono.Addins;

using ZencomicLib;

namespace GarfieldMinusGarfieldComicAddin
{
	[Extension ("/Zencomic/ComicAddins")]
	public class GarfieldMinusGarfieldComicAddin : IComicAddin
	{
		const string baseUrl = "http://garfieldminusgarfield.net/";
		const string imageUrl = baseUrl + "page/{0}";
		
		Random rand = new Random ();
		WebClient client = new WebClient ();
		int? LastValidPage {
			get; set;
		}
		
		#region IComicAddin implementation
		public Gdk.Pixbuf GetNextComic (out string url)
		{
			url = string.Format (imageUrl, rand.Next (0, GetLastValidPage ()));
			
			string page = client.DownloadString (url);
			List <string> potentials = new List<string> ();
			string [] parts = page.Split (new string [] { "<div class=\"photo\">" }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 1; i < parts.Length; i++) {
				string s = parts [i];
				s = s.Substring (s.IndexOf ("<img src=\"") + "<img src=\"".Length);
				s = s.Substring (0, s.IndexOf ('"'));
				potentials.Add (s);
			}
			
			url = potentials [rand.Next (0, potentials.Count)];
			return new Gdk.Pixbuf (client.OpenRead (url));
		}

		int GetLastValidPage ()
		{
			if (LastValidPage.HasValue)
				return LastValidPage.Value;
			
			int result = 100;
			string page = client.DownloadString (baseUrl);
			Regex r = new Regex (@"<a href=""/page/([\d]+)\"" id=\""lastpage\"">First Comics</a>");
			var m = r.Match (page);
			if (m == null || m.Groups.Count != 2) {
				Console.WriteLine ("No match. There were {0} groups but expected 2", m == null ? 0 : m.Groups.Count);
				return 100;
			} else {
				if (int.TryParse (m.Groups[1].Value, out result)) {
					Console.WriteLine ("Detected last valid page as: {0}", result);
					LastValidPage = result;
				}
			}
			return result;
		}

		public string ComicName {
			get {
				return "GarfieldMinusGarfield";
			}
		}
		
		public string ComicAuthor {
			get {
				return "Jim Davis/Dan Walsh";
			}
		}
		
		public bool ShouldCachePictures {
			get;
			set;
		}
		#endregion
	}
}

