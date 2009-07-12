// 
// DilbertComicAddin.cs
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
using System.Net;
using System.Text.RegularExpressions;

using Gdk;
using Mono.Addins;

using ZencomicLib;

namespace DilbertComicAddin
{
	
	[Extension ("/Zencomic/ComicAddins")]
	public class DilbertComicAddin : IComicAddin
	{
		const string baseUrl = "http://dilbert.com";
		readonly DateTime baseDate = new DateTime (1994, 1, 23);
		
		Random rand = new Random ();
		DateTime today = DateTime.Today;
		WebClient client = new WebClient ();
		Regex r = new Regex ("/dyn/str_strip/(.+)\\.strip\\.gif", RegexOptions.Compiled);
		
		DateTime GetRandomDateTime ()
		{
			int year = rand.Next (baseDate.Year, today.Year + 1);			
			int month = year == today.Year ? rand.Next (1, today.Month + 1) : rand.Next (1, 13);
			int day = year == today.Year ? rand.Next (1, today.Day + 1) : rand.Next (1, 30);
			
			return new DateTime (year, month, day);
		}
		
		string GetHtmlPage (DateTime date)
		{
			string url = string.Format ("http://dilbert.com/strips/comic/{0}-{1}-{2}/",
			                            date.Year,
			                            date.Month,
			                            date.Day);
			
			return client.DownloadString (url);
		}
		
		string GetUrlForPage (string page)
		{	
			Match m = r.Match (page);
						
			return m.Captures[0].Value;
		}
		
		#region IComicAddin implementation
		public Gdk.Pixbuf GetNextComic ()
		{
			string page = GetHtmlPage (GetRandomDateTime ());
			string url = GetUrlForPage (page);
			Console.WriteLine ("Dilbert comic url : " + baseUrl + url);
			
			Stream image = client.OpenRead (baseUrl + url);
			
			return new Gdk.Pixbuf (image);
		}
		
		public string ComicName {
			get {
				return "Dilbert";
			}
		}
		
		public string ComicAuthor {
			get {
				return "Scott Adams";
			}
		}
		
		public bool ShouldCachePictures {
			get;
			set;
		}
		#endregion
	}
}
