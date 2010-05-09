// 
// GarfieldComicAddin.cs
//  
// Author:
//       Jérémie Laval <jeremie.laval@gmail.com>
// 
// Copyright (c) 2010 Jérémie "Garuma" Laval
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
using System.Net;
using System.Text.RegularExpressions;

using Gdk;
using Mono.Addins;

using ZencomicLib;

namespace GarfieldComicAddin
{
	[Extension ("/Zencomic/ComicAddins")]
	public class GarfieldComicAddin : IComicAddin
	{
		const string baseUrl = "http://images.ucomics.com/comics/ga/{0}/ga{1}.gif";
		readonly DateTime baseDate = new DateTime (1979, 1, 1);
		
		Random rand = new Random ();
		DateTime today = DateTime.Today;
		WebClient client = new WebClient ();
		
		DateTime GetRandomDateTime ()
		{
			int year = rand.Next (baseDate.Year, today.Year + 1);			
			int month = year == today.Year ? rand.Next (1, today.Month + 1) : rand.Next (1, 13);
			int day = year == today.Year ? rand.Next (1, today.Day + 1) : rand.Next (1, 30);
			
			return new DateTime (year, month, day);
		}
		
		#region IComicAddin implementation
		public Gdk.Pixbuf GetNextComic (out string url)
		{
			DateTime d = GetRandomDateTime ();
			url = string.Format (baseUrl, d.Year, d.ToString ("yyMMdd"));
			Console.WriteLine ("Using " + url);
			
			Stream image = client.OpenRead (url);
			return new Gdk.Pixbuf (image);
		}
		
		public string ComicName {
			get {
				return "Garfield";
			}
		}
		
		public string ComicAuthor {
			get {
				return "Jim Davis";
			}
		}
		
		public bool ShouldCachePictures {
			get;
			set;
		}
		#endregion
	}
}

