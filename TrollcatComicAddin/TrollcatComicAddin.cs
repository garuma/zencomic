// 
// TrollcatComicAddin.cs
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
using System.Net;
using System.Text.RegularExpressions;

using Gdk;
using Mono.Addins;

using ZencomicLib;

namespace TrollcatComicAddin
{
	
	[Extension ("/Zencomic/ComicAddins")]
	public class TrollcatComicAddin : IComicAddin
	{
		Regex random = new Regex ("http://trollcats.com/(\\d+)/(\\d+)/(\\w|-|_)+/", RegexOptions.Compiled);
		Regex r = new Regex ("http://trollcats.com/wp-content/uploads/(\\d+)/(\\d+)/(\\w|-|_)+\\.(png|jpg|gif)", RegexOptions.Compiled);
		const string randomUrl = "http://trollcats.com";
		
		WebClient client = new WebClient ();

		#region IComicAddin implementation
		public Pixbuf GetNextComic (out string url)
		{
			url = string.Empty;
			
			string page = client.DownloadString (randomUrl);

			Match m = random.Match (page);
			if (m == null || m.Captures.Count == 0)
				return null;
			
			string realRandomUrl = m.Captures [0].Value;

			return ComicAddinHelper.RegexBasedRetrieval (r, realRandomUrl, out url);
		}
		
		public string ComicName {
			get {
				return "Trollcat";
			}
		}
		
		public string ComicAuthor {
			get {
				return "Various";
			}
		}
		
		public bool ShouldCachePictures {
			get;
			set;
		}
		#endregion
	}
}