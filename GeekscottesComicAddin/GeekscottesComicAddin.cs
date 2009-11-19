// 
// GeekscottesComicAddin.cs
//  
// Author:
//       Frédéric "GeoVah" Forjan <fforjan@free.fr>
// 
// Copyright (c) 2009 Frédéric "GeoVah" Forjan
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

namespace GeekscottesComicAddin
{
	
	[Extension ("/Zencomic/ComicAddins")]
	public class GeekscottesComicAddin : IComicAddin
	{
		//regexp for finding the image in the page
		Regex imgRegExp = new Regex ("./strips/(.+)\\.png", RegexOptions.Compiled);
		
		//regexp for first ID
		Regex firstIDRegExp = new Regex ("id=\"navfirst\" href=\"index.php\\?strip=(\\d+)\"", RegexOptions.Compiled);
		
		//regexp for last ID
		Regex lastIDRegExp = new Regex ("id=\"navlast\" href=\"index.php\\?strip=(\\d+)\"", RegexOptions.Compiled);
		
		//retrieve a page from a specific ID
		const string pageUrlFromId = "http://www.nojhan.net/geekscottes/index.php?strip={0}";
                
		//index page
		const string indexUrl      = "http://www.nojhan.net/geekscottes/index.php";
		WebClient client = new WebClient ();
                
		//today strip don't support random  so we need a generator
		Random randObj = new Random();
		
		//Retrieve the ID marched by firstIDRegExp or lastIDRegExp
		private string GetID(string regExpValue)
		{
			int lastIndex = regExpValue.LastIndexOf('=') + 1;
			string res = regExpValue.Substring(lastIndex, (regExpValue.Length - lastIndex -1 ));
			
			return res;
		}
		
		//This function return the url of page to be use a random page
		private string GetPageUrl()
		{
			string index = client.DownloadString(indexUrl);
			Match firstMatch = firstIDRegExp.Match(index);
			Match lastMatch  = lastIDRegExp.Match(index);
			
			if ((firstMatch == null || firstMatch.Captures.Count == 0)||
			    (lastMatch == null || lastMatch.Captures.Count == 0))
			{
				Console.WriteLine("nothing matching url!");
				//We will show the latest image in this case...
				return indexUrl;
			}
			
			int first = int.Parse(GetID(firstMatch.Value));
			int last  = int.Parse(GetID(lastMatch.Value));
			
			return String.Format(pageUrlFromId,randObj.Next(first,last));
		}
		#region IComicAddin implementation
		public Pixbuf GetNextComic (out string url)
		{
			url = string.Empty;
			string pageurl =  GetPageUrl();
			
			string page = client.DownloadString (pageurl);
			
			Match imageMatch = imgRegExp.Match (page);
			if (imageMatch == null || imageMatch.Captures.Count == 0)
			{
				Console.WriteLine("nothing matching url!");
				return null;
			}
			url = "http://www.nojhan.net/geekscottes/" + imageMatch.Captures [0].Value;
			
			return new Gdk.Pixbuf (client.OpenRead (url));
		}
		
		public string ComicName {
			get {
				return "Geekscottes";
			}
		}
		
		public string ComicAuthor {
			get {
				return "Nojhan";
			}
		}
		
		public bool ShouldCachePictures {
			get;
			set;
		}
		#endregion
	}
}
